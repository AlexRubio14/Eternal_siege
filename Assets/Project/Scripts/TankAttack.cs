using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] private float offset;

    [SerializeField] private float distance;

    private void OnEnable()
    {
        Vector3 direction;
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

        transform.localPosition = direction * offset;
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Do damage
        }
    }
}
