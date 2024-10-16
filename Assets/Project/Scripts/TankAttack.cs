using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] private Tank tank;

    [SerializeField] private float damage;

    [SerializeField] private float initialZ;
    [SerializeField] private float finalZ;
    [SerializeField] private float duration;

    private float timer;

    private void OnEnable()
    {
        timer = 0;
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            initialZ);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;

        transform.localPosition = new Vector3(
            transform.localPosition.x, 
            transform.localPosition.y,
            Mathf.Lerp(initialZ, finalZ, t));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision is BoxCollider)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(damage);
        }
    }
}
