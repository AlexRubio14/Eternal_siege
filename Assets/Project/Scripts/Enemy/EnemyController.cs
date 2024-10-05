using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject target;

    [SerializeField] private float distanceSeparation;

    [SerializeField] private float cohesionWeight;
    [SerializeField] private float separationWeight;
    [SerializeField] private float alligmentWeight;

    private Vector2 direction;
    private Vector2 separationForce;

    private void Awake()
    {
        direction = Vector2.zero;
    }

    private void Update()
    {
        if(target != null) 
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        separationForce = Vector2.zero;
        direction = (target.transform.position - transform.position);

        Collider2D[] neighbours = GetNeighbours();

        if(neighbours.Length > 0) 
        {
            CalculateSeparationFoce(neighbours);
            ApplyAlligment(neighbours);
            ApplyCohesion(neighbours);
        }

        MoveEnemy();
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
        return Physics2D.OverlapCircleAll(transform.position, distanceSeparation, enemyMask);
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

        if(neighboursForward != Vector3.zero)
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

}
