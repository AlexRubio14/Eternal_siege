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
    [SerializeField] private Vector3 separationDistance;
    protected Vector3 direction;
    protected bool canMove;
    protected Rigidbody rgbd;

    [Header("Forces")]
    [SerializeField] private float cohesionWeight;
    [SerializeField] private float separationWeight;
    [SerializeField] private float alligmentWeight;
    private Vector3 separationForce;

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
        rgbd = GetComponent<Rigidbody>();

        currentHP = maxHP;
        canMove = true;
        direction = Vector3.zero;
    }

    protected virtual void Update()
    {
        if (target.Count > 0 && canMove)
        {
            Seek();
            CalculateForces();
            Rotate();
            MoveEnemy();
        }
        else if (!canMove)
            rgbd.velocity = Vector3.zero;
    }
    private void Seek()
    {
        for (int i = target.Count - 1; i >= 0; i--)
        {
            if (target[i].GetComponent<PlayerController>().GetCurrentState() != PlayerController.State.DEAD)
            {
                direction = target[i].transform.localPosition - transform.localPosition;
                currentTarget = target[i];
            }
        }

        for (int i = 0; i < target.Count; i++)
        {
            Vector3 distancePostion1 = target[i].transform.localPosition - transform.localPosition;
            if (distancePostion1.magnitude < direction.magnitude && target[i].GetComponent<PlayerController>().GetCurrentState() != PlayerController.State.DEAD)
            {
                direction = distancePostion1;
                currentTarget = target[i];
            }
        }
    }

    private void MoveEnemy()
    {
        Vector3 combinedDirection = (direction.normalized + separationForce).normalized;
        Vector3 movement = combinedDirection * speed * Time.deltaTime * TimeManager.instance.GetPaused();
        rgbd.AddForce(movement, ForceMode.Force);
    }

    private void CalculateForces()
    {
        separationForce = Vector3.zero;
        Collider[] neighbours = GetNeighbours();

        if (neighbours.Length > 0)
        {
            CalculateSeparationFoce(neighbours);
            ApplyAlligment(neighbours);
            ApplyCohesion(neighbours);
        }
    }

    private Collider[] GetNeighbours()
    {
        LayerMask enemyMask = LayerMask.GetMask("Enemy");
        return Physics.OverlapBox(transform.position, separationDistance, Quaternion.identity, enemyMask);
    }
    private void CalculateSeparationFoce(Collider[] neighbours)
    {
        foreach (var neighbour in neighbours)
        {
            Vector3 direction = neighbour.transform.position - transform.position;
            float distance = direction.magnitude;
            Vector3 away = -direction.normalized;

            if (distance > 0)
            {
                separationForce += away / distance * separationWeight;
            }
        }
    }

    private void ApplyAlligment(Collider[] neighbours)
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

        separationForce += neighboursForward * alligmentWeight;
    }
    private void ApplyCohesion(Collider[] neighbours)
    {
        Vector3 avaragePosition = Vector3.zero;

        foreach (var neighbour in neighbours)
        {
            avaragePosition += neighbour.transform.position;
        }

        avaragePosition /= neighbours.Length;
        Vector3 cohesionDirection = (avaragePosition - transform.position).normalized;
        separationForce += cohesionDirection * cohesionWeight;
    }

    private void Rotate()
    {
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        }
    }

    public void ReceiveDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            animator.SetBool("Die", true);
            canMove = false;
            rgbd.velocity = Vector3.zero;
            GenerateExperienceBall();
            GetComponent<BoxCollider>().enabled = false;
            EnemyManager.instance.GetEnemies().Remove(gameObject);
            Invoke("Die", animator.GetCurrentAnimatorStateInfo(0).length / animationDivide);
        }
    }

    private void GenerateExperienceBall()
    {
        GameObject _experienceBall = Instantiate(experienceBall, new Vector3(transform.localPosition.x, transform.localPosition.y, -2.5f), Quaternion.identity);
        _experienceBall.GetComponent<ExperienceBall>().SetExperience(experience);
    }
    protected virtual void Die()
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
