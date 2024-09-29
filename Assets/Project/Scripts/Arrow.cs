using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float speed = 10f;

    [Range(0f, 10f)]
    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private float damage;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = transform.up * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Do damage
            Destroy(gameObject);
        }
    }

}
