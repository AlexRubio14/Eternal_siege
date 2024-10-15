using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] private Tank tank;

    [SerializeField] private float damage;

    [SerializeField] private float offset;
    [SerializeField] private float distance;
    private Vector3 direction;

    private void OnEnable()
    {
        //if (EnemyManager.instance.GetEnemies().Count > 0)
        //{
        //    Vector2 nearestEnemyDirection;
        //    Vector2 nearestEnemyPosition;

        //    Vector2 attackPosition = new Vector2(transform.localPosition.x, transform.localPosition.z);

        //    EnemyManager.instance.GetNearestEnemyDirection(attackPosition, out nearestEnemyDirection, out nearestEnemyPosition);
        //    if ((nearestEnemyPosition - new Vector2(transform.localPosition.x, transform.localPosition.z)).magnitude < distance)
        //        direction = new Vector3(nearestEnemyDirection.x, 0, nearestEnemyDirection.y);
        //    else
        //        direction = Vector3.right;
        //}
        //else
        //    direction = Vector3.right;

        //transform.localPosition = direction * offset;
        //transform.up = direction;
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
