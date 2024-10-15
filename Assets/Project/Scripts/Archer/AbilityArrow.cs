using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityArrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float distance;

    private Vector2 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        transform.up = direction;
    }

    private void FixedUpdate()
    {
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
}
