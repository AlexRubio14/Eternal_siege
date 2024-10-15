using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UltimateArrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private float damage;

    [SerializeField] private float distance;

    private Vector2 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        if (EnemyManager.instance.GetEnemies().Count > 0)
        {
            Vector3 nearestEnemyDirection;
            Vector3 nearestEnemyPosition;

            EnemyManager.instance.GetNearestEnemyDirection(transform.position, out nearestEnemyDirection, out nearestEnemyPosition);
            if ((nearestEnemyPosition - new Vector3(transform.position.x, 0, transform.position.z)).magnitude < distance)
                direction = nearestEnemyDirection;
            else
                direction = Vector3.right;
        }
        else
            direction = Vector3.right;

        transform.up = direction;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(damage);
        }
    }
}