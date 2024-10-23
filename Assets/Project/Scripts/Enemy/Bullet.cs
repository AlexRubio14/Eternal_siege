using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (TimeManager.instance.GetPaused() != 0)
                collision.gameObject.GetComponent<PlayerController>().ReceiveDamage(damage);
            Debug.Log("bullet");
            Destroy(gameObject);
        }
    }
}
