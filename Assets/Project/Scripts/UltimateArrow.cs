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
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        if (EnemyManager.instance.GetEnemies().Count > 0)
        {
            Vector2 nearestEnemyDirection;
            Vector2 nearestEnemyPosition;

            EnemyManager.instance.GetNearestEnemyDirection(transform.position, out nearestEnemyDirection, out nearestEnemyPosition);
            if ((nearestEnemyPosition - new Vector2(transform.position.x, transform.position.y)).magnitude < distance)
                direction = nearestEnemyDirection;
            else
                direction = Vector2.right;
        }
        else
            direction = Vector2.right;

        transform.up = direction;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = direction * speed * Time.deltaTime;
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
