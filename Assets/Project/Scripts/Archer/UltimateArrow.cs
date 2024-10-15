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

    private Vector3 direction;
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
            Vector2 nearestEnemyDirection;
            Vector2 nearestEnemyPosition;

            Vector2 arrowPosition = new Vector2(transform.localPosition.x, transform.localPosition.z);

            EnemyManager.instance.GetNearestEnemyDirection(arrowPosition, out nearestEnemyDirection, out nearestEnemyPosition);
            if ((nearestEnemyPosition - new Vector2(transform.localPosition.x, transform.localPosition.z)).magnitude < distance)
                direction = new Vector3(nearestEnemyDirection.x, 0, nearestEnemyDirection.y);
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(damage);
        }
    }
}
