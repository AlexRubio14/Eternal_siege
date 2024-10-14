using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float distance;
    [SerializeField] private bool isAbility;

    private Vector2 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        if (!isAbility)
        {
            AimToEnemy();
        }
    }
    
    private void FixedUpdate()
    {
        if(isAbility)
            rb.velocity = transform.up * speed * Time.deltaTime;
        else
            rb.velocity = direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            damage *= RogueliteManager.instance.GetDamageMultiplier();
            enemy.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }

    private void AimToEnemy()
    {
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
}
