using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float lifeTime;

    [SerializeField] private float damage;

    [SerializeField] private float distance;

    private Vector2 direction;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>()  ;
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
        //transform.Rotate(0, 0, direction.z);
        rb2d.velocity = direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
