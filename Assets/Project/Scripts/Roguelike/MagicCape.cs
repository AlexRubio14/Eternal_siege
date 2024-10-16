
using UnityEngine;

public class MagicCape : MonoBehaviour
{
    [SerializeField] private float initScale;
    [SerializeField] private float augmentScale;
    [SerializeField] private float damage;
    [SerializeField] private float augmentDamage;
    [SerializeField] private float timeToActivate;

    private SphereCollider sphereCollider;
    private float timer;

    private int level;

    private void Awake()
    {
        level = 1;
        timer = 0;
        transform.localScale = new Vector3(initScale, transform.localScale.y, initScale);
        sphereCollider.enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToActivate)
        {
            sphereCollider.enabled = true;
            Invoke("DisableCollision", 0.1f);
            //un vfx q resalte q este es el instante en el q daña
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other is BoxCollider)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(damage);
        }
    }

    private void DisableCollision()
    {
        sphereCollider.enabled = false;
    }

    public void LevelUp()
    {
        level += 1;

        if (level < 5)
        {
            transform.localScale = new Vector3(
                transform.localScale.x + augmentScale, 
                transform.localScale.y, 
                transform.localScale.z + augmentScale
                );
        }
        else
        {
            transform.localScale = new Vector3(
                transform.localScale.x + augmentScale * 2,
                transform.localScale.y,
                transform.localScale.z + augmentScale * 2
                );
        }
    }
    
}
