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

    private Vector3 direction;
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
            rb.velocity = transform.up * speed * Time.deltaTime * TimeManager.instance.GetPaused();
        else
            rb.velocity = direction * speed * Time.deltaTime * TimeManager.instance.GetPaused();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision is BoxCollider)
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
            Vector2 nearestEnemyDirection;
            Vector2 nearestEnemyPosition;

            Vector2 arrowPosition = new Vector2(transform.localPosition.x, transform.localPosition.z);

            EnemyManager.instance.GetNearestEnemyDirection(arrowPosition, out nearestEnemyDirection, out nearestEnemyPosition);
            if ((nearestEnemyPosition - arrowPosition).magnitude < distance)
                direction = new Vector3(nearestEnemyDirection.x, 0, nearestEnemyDirection.y);
            else
                direction = Vector3.right;
        }
        else
            direction = Vector3.right;

        transform.up = direction;
    }
}
