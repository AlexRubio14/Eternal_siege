using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Inputs"), SerializeField]
    private InputActionReference movementAction;

    [SerializeField]
    private float speed;

    private Vector2 movementDirection;

    private Rigidbody2D rb2d;

    private SpriteRenderer sprite;

    [SerializeField]
    private int playerIndex;

    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();
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

    public int GetPlayerIndex()
    {
        return playerIndex;
    }
}