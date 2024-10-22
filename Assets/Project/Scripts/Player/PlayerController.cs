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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        transform.position = PlayersManager.instance.posToSpawnList[0].position;

        isShielding = false;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
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
                break;
            case State.DEAD:
                break;
            default:
                break;
        }
    }

    private void CheckIdleOrMovingState()
    {
        if(inputMovementDirection == Vector2.zero && currentState == State.MOVING)
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

        ChangeState(State.INVENCIBILITY);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(StopInvencibility());
    }

    private IEnumerator StopInvencibility()
    {
        yield return new WaitForSeconds(invencibilityTime);

        Debug.Log("Invencibility time ended");
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
    }

    public void SetPlayerInformation(PlayerInformation _playerInformation)
    {
        playerInformation = _playerInformation;
    }

    public void SetIsShielding(bool _isShielding)
    {
        isShielding = _isShielding;
    }
}
