using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Inputs"), SerializeField]
    private InputActionReference movementAction;

    public enum State { IDLE, MOVING, KNOCKBACK, INVENCIBILITY, DEAD };

    private State currentState;

    private float speed;

    private Vector2 inputMovementDirection;
    private Vector3 movementDirection;

    private Rigidbody rb;

    private SpriteRenderer sprite;

    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool isInArea;

    [SerializeField] private float invencibilityTime;

    [SerializeField] private GameObject reviveRadius;

    private Animator anim;
    private PlayerInformation playerInformation;

    [SerializeField] private bool isShielding;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private float changeColorVelocity;
    private Color startColor;

    [SerializeField] private float vibrationDuration;
    [SerializeField] private float lowFrequencyVibration; 
    [SerializeField] private float highFrequencyVibration;

    private Gamepad playerGamepad;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        transform.position = PlayersManager.instance.posToSpawnList[0].position;

        isShielding = false;
        startColor = meshRenderer.material.color;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(playerInformation.GetValue().ToString() != (currentHealth/maxHealth).ToString())
        {
            playerInformation.SetHPBar(currentHealth / maxHealth);
        }

        switch (currentState)
        {
            case State.IDLE:
                CheckIdleOrMovingState();
                break;
            case State.MOVING:
                Move();
                Rotate();
                CheckIdleOrMovingState();
                break;
            case State.KNOCKBACK:
                break;
            case State.INVENCIBILITY:
                Move();
                Rotate();
                CheckIdleOrMovingState();
                InvisibilityColorChange();
                break;
            case State.DEAD:
                break;
            default:
                break;
        }
    }

    private void CheckIdleOrMovingState()
    {
        if(inputMovementDirection == Vector2.zero && (currentState == State.MOVING || currentState == State.INVENCIBILITY))
        {
            ChangeState(State.IDLE);
            anim.SetBool("Walking", false);
        }
        else if(inputMovementDirection != Vector2.zero && currentState == State.IDLE)
        {
            ChangeState(State.MOVING);
            anim.SetBool("Walking", true);
        }
    }

    private void InvisibilityColorChange()
    {
        float pingPongValue = Mathf.PingPong(Time.time * changeColorVelocity, 1f);
        Color color = Color.Lerp(startColor, Color.gray, pingPongValue);
        meshRenderer.material.color = color;
    }

    public void ChangeState(State state)
    {
        switch (currentState)
        {
            case State.IDLE:
                break;
            case State.MOVING:
               
                break;
            case State.KNOCKBACK:
                break;
            case State.INVENCIBILITY:
                break;
            case State.DEAD:
                break;
            default:
                break;
        }

        switch (state)
        {
            case State.IDLE:
                break;
            case State.MOVING:
                
                break;
            case State.KNOCKBACK:
                break;
            case State.INVENCIBILITY:
                break;
            case State.DEAD:
                break;
            default:
                break;
        }

        currentState = state;
    }

    private void Move()
    {
        movementDirection = new Vector3(inputMovementDirection.x, 0, inputMovementDirection.y);
        rb.AddForce(movementDirection * speed * Time.deltaTime * TimeManager.instance.GetPaused(), ForceMode.Force);
    }

    private void Rotate()
    {
        if(inputMovementDirection != Vector2.zero)
        {
            Vector3 movementDirection = new Vector3(inputMovementDirection.x, 0, inputMovementDirection.y);
            transform.forward = movementDirection;
        }

    }

    public void MovementAction(InputAction.CallbackContext obj)
    {
        if (currentState == State.DEAD || currentState == State.KNOCKBACK)
            return;

        inputMovementDirection = obj.action.ReadValue<Vector2>();
    }

    public void ReceiveDamage(float damage)
    {
        if (currentState != State.MOVING && currentState != State.IDLE)
            return;

        currentHealth -= damage;

        playerInformation.SetHPBar(currentHealth / maxHealth);
        Debug.Log("receiveDamage");

        ChangeState(State.INVENCIBILITY);

        TriggerVibration(lowFrequencyVibration, highFrequencyVibration, vibrationDuration);

        if (currentHealth <= 0)
        {
            Die();
            playerGamepad.SetMotorSpeeds(0, 0);
            return;
        }

        StartCoroutine(StopInvencibility());
    }

    private void TriggerVibration(float lowFrequency, float highFrequency, float duration)
    {
        playerGamepad.SetMotorSpeeds(lowFrequency, highFrequency);

        StartCoroutine(StopVibrationAfterDelay(duration));
    }

    private IEnumerator StopVibrationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        playerGamepad.SetMotorSpeeds(0, 0);
    }

    private IEnumerator StopInvencibility()
    {
        yield return new WaitForSeconds(invencibilityTime);

        Debug.Log("Invencibility time ended");
        meshRenderer.material.color = startColor;
        ChangeState(State.MOVING);
    }

    public void Die()
    {
        ChangeState(State.DEAD);

        PlayersManager.instance.CheckIfAllPLayersDead();

        reviveRadius.gameObject.SetActive(true);
    }

    public void Revive()
    {
        currentHealth = maxHealth;

        playerInformation.SetHPBar(currentHealth / maxHealth);
        Debug.Log("revive");

        ChangeState(State.INVENCIBILITY);
        
        StartCoroutine(StopInvencibility());
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.collider is BoxCollider && !isShielding)
        {
            ReceiveDamage(collision.gameObject.GetComponent<Enemy>().GetDamage());
            Debug.Log("enemy");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AttackArea"))
        {
            isInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AttackArea"))
        {
            isInArea = false;
        }
    }

    public bool GetIsInArea()
    {
        return isInArea;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void AddMaxHealth(float _health)
    {
        currentHealth += _health;
        maxHealth += _health;
        playerInformation.SetHPBar(currentHealth / maxHealth);
    }

    public void SetPlayerInformation(PlayerInformation _playerInformation)
    {
        playerInformation = _playerInformation;
    }

    public void SetIsShielding(bool _isShielding)
    {
        isShielding = _isShielding;
    }

    public void SetPlayerGamePad(Gamepad gamepad)
    {
        playerGamepad = gamepad;
    }
}
