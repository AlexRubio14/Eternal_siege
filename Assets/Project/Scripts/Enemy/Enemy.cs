using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public abstract class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float speed;
    [SerializeField] private Vector2 separationDistance;
    protected Vector2 direction;
    protected bool canMove;
    protected Rigidbody2D rgbd2d;

    [Header("Forces")]
    [SerializeField] private float cohesionWeight;
    [SerializeField] private float separationWeight;
    [SerializeField] private float alligmentWeight;
    private Vector2 separationForce;

    [Header("Combat")]
    [SerializeField] protected int maxHP;
    [SerializeField] protected float damage;
    [SerializeField] protected float experience;
    [SerializeField] protected GameObject experienceBall;
    protected float currentHP;
    protected List<GameObject> target;
    protected GameObject currentTarget;

    [Header("Animation")]
    [SerializeField] protected float animationDivide;
    protected Animator animator;

    protected void Initialize()
    {
        animator = GetComponent<Animator>();
        rgbd2d = GetComponent<Rigidbody2D>();

        currentHP = maxHP;
        canMove = true;
        direction = Vector2.zero;
    }

    protected virtual void Update()
    {
        if (target.Count > 0 && canMove)
            Seek();
        else if(!canMove)
            rgbd2d.velocity = Vector3.zero;
    }
    private void Seek()
    {
        separationForce = Vector2.zero;

        direction = (Vector2)target[0].transform.localPosition - (Vector2)transform.localPosition;
        currentTarget = target[0];
        for (int i = 1; i < target.Count; i++)
        {
            Vector2 distancePostion1 = (Vector2)target[i].transform.localPosition - (Vector2)transform.localPosition;
            if (distancePostion1.magnitude < direction.magnitude)
            {
                direction = distancePostion1;
                currentTarget = target[i];
            }
        }

        Collider2D[] neighbours = GetNeighbours();

        if (neighbours.Length > 0)
        {
            CalculateSeparationFoce(neighbours);
            ApplyAlligment(neighbours);
            ApplyCohesion(neighbours);
        }

        MoveEnemy();

        Rotate();
    }

    private void MoveEnemy()
    {
        Vector2 combinedDirection = (direction.normalized + separationForce).normalized;
        Vector2 movement = combinedDirection * speed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0);
    }

    private Collider2D[] GetNeighbours()
    {
        LayerMask enemyMask = LayerMask.GetMask("Enemy");
        return Physics2D.OverlapBoxAll(transform.position, separationDistance, 0, enemyMask);
    }
    private void CalculateSeparationFoce(Collider2D[] neighbours)
    {
        foreach (var neighbour in neighbours)
        {
            Vector2 direction = neighbour.transform.position - transform.position;
            float distance = direction.magnitude;
            Vector2 away = -direction.normalized;

            if (distance > 0)
            {
                separationForce += away / distance * separationWeight;
            }
        }
    }

    private void ApplyAlligment(Collider2D[] neighbours)
    {
        Vector3 neighboursForward = Vector3.zero;

        foreach (var neighbour in neighbours)
        {
            neighboursForward += neighbour.transform.forward;
        }

        if (neighboursForward != Vector3.zero)
        {
            neighboursForward.Normalize();
        }

        separationForce += new Vector2(neighboursForward.x, neighboursForward.y) * alligmentWeight;
    }
    private void ApplyCohesion(Collider2D[] neighbours)
    {
        Vector2 avaragePosition = Vector2.zero;

        foreach (var neighbour in neighbours)
        {
            avaragePosition += new Vector2(neighbour.transform.position.x, neighbour.transform.position.y);
        }

        avaragePosition /= neighbours.Length;
        Vector2 cohesionDirection = (avaragePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        separationForce += cohesionDirection * cohesionWeight;
    }

    private void Rotate()
    {
        transform.up = direction;
    }

    public void ReceiveDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            animator.SetBool("Die", true);
            canMove = false;
            rgbd2d.velocity = Vector3.zero;
            GameObject _experienceBall = Instantiate(experienceBall, new Vector3(transform.localPosition.x, transform.localPosition.y, -2.5f), Quaternion.identity);
            _experienceBall.GetComponent<ExperienceBall>().SetExperience(experience);
            GetComponent<BoxCollider2D>().enabled = false;
            EnemyManager.instance.GetEnemies().Remove(gameObject);
            Invoke("Die", animator.GetCurrentAnimatorStateInfo(0).length / animationDivide);
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    public void SetTarget(List<GameObject> _target)
    {
        target = _target;
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }

    public float GetDamage()
    {
        return damage;
    }


}
