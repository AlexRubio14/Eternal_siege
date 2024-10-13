using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] private Tank tank;

    [SerializeField] private float damage;

    [SerializeField] private float offset;
    [SerializeField] private float distance;

    private void OnEnable()
    {
        Vector3 direction;
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

        transform.localPosition = direction * offset;
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(damage);
        }
    }
}
