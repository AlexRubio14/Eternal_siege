using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateArrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private float damage;

    private Vector3 direction;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        if (EnemyManager.instance.GetEnemies().Count > 0)
            direction = EnemyManager.instance.GetNearestEnemyDirection(transform.position);
        else
            direction = Vector3.right;

        //transform.Rotate(direction.x, direction.y, direction.z);
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
            enemy.ReciveDamage(damage);
        }
    }
}
