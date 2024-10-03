using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] private float offset;

    private void OnEnable()
    {
        Vector3 direction;
        if (EnemyManager.instance.GetEnemies().Count > 0)
            direction = EnemyManager.instance.GetNearestEnemyDirection(transform.position);
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
