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

    [SerializeField]
    private float speed;

    private Vector2 movementDirection;

    private Rigidbody2D rb2d;

    private SpriteRenderer sprite;

    [SerializeField]
    private float startHealth;
    private float currentHealth;

    [SerializeField]
    private Archer archer;

    [SerializeField] private float invencibilityTime;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        currentHealth = startHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayersManager.instance.AddPlayer(gameObject);
    }

    // Update is called once per frame
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
        }
        else if(movementDirection != Vector2.zero && currentState == State.IDLE)
        {
            ChangeState(State.MOVING);
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
        currentState = state;
    }

    private void Move()
    {
        rb2d.AddForce(movementDirection * speed * Time.deltaTime, ForceMode2D.Force);
    }

    private void Rotate()
    {
        if(movementDirection.x > 0)
            sprite.flipX = true; 
        else if(movementDirection.x < 0)
            sprite.flipX = false;
    }

    public void MovementAction(InputAction.CallbackContext obj)
    {
        movementDirection = obj.action.ReadValue<Vector2>();
    }

    public void BasicAbilityAction(InputAction.CallbackContext obj)
    {
        if (currentState == State.KNOCKBACK || currentState == State.DEAD)
            return;

        archer.Ability();
    }

    public void UltimateAbilityAction(InputAction.CallbackContext obj)
    {
        if (currentState == State.KNOCKBACK || currentState == State.DEAD)
            return;

        archer.Ultimate();
    }


    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ChangeState(State.DEAD);

        PlayersManager.instance.CheckIfAllPLayersDead();

        Invoke("Revive", 2.0f);
    }

    private void Revive()
    {
        currentHealth = startHealth;
        Debug.Log(currentHealth);

        SetCurrentState(State.INVENCIBILITY);
        
        Invoke("EndInvencibility", invencibilityTime);
    }

    private void EndInvencibility()
    {
        Debug.Log("Invencibility time ended");
        SetCurrentState(State.MOVING);
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState(State newState)
    {
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != State.MOVING)
            return;

        if (collision.CompareTag("Enemy"))
        {
            ReceiveDamage(collision.GetComponent<Enemy>().GetDamage());
        }
    }
}
