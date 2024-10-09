using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
    [Header("Inputs"), SerializeField]
    private InputActionReference movementAction;

    public enum State { IDLE, MOVING, KNOCKBACK, INVENCIBILITY, DEAD };

    private State currentState;

    private float speed;

    private Vector2 movementDirection;

    private Rigidbody2D rb2d;

    private SpriteRenderer sprite;

    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool isInArea;

    [SerializeField] private float invencibilityTime;

    [SerializeField] private GameObject reviveRadius;

    private Animator anim;
    private PlayerInformation playerInformation;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //speed = -1f;
        //currentHealth = -1f;
    }

    void Start()
    {
        PlayersManager.instance.AddPlayer(gameObject);
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
        if(movementDirection == Vector2.zero && currentState == State.MOVING)
        {
            ChangeState(State.IDLE);
            anim.SetBool("Walking", false);
        }
        else if(movementDirection != Vector2.zero && currentState == State.IDLE)
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
                //sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255); 
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
        //rb2d.AddForce(movementDirection * speed * Time.deltaTime, ForceMode2D.Force);
        rb2d.velocity = movementDirection * speed;
    }

    private void Rotate()
    {
        if(movementDirection != Vector2.zero)
        {
            transform.up = movementDirection;
        }

    }

    public void MovementAction(InputAction.CallbackContext obj)
    {
        if (currentState == State.DEAD || currentState == State.KNOCKBACK)
            return;

        movementDirection = obj.action.ReadValue<Vector2>();
    }

    public void ReceiveDamage(float damage)
    {
        if (currentState != State.MOVING && currentState != State.IDLE)
            return;

        currentHealth -= damage;
        Debug.Log(currentHealth);

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

        Debug.Log(currentHealth);

        ChangeState(State.INVENCIBILITY);
        
        StartCoroutine(StopInvencibility());
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision is BoxCollider2D)
        {
            ReceiveDamage(collision.GetComponent<Enemy>().GetDamage());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackArea"))
        {
            isInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackArea"))
        {
            isInArea = false;
        }
    }

    public bool GetIsInArea()
    {
        return isInArea;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetMaxHealth(float _health)
    {
        currentHealth += (_health - maxHealth);
        maxHealth = _health;
    }

    public void SetPlayerInformation(PlayerInformation _playerInformation)
    {
        playerInformation = _playerInformation;
    }
}
