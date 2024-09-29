using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float maxTime;
    [SerializeField] private float speed;

    private bool canShoot;
    private float time;
    private GameObject target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Enemy>().SetCanMove(false);
            canShoot = true;
            time = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Enemy>().SetCanMove(true);
            canShoot = false;
            time = 0;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (canShoot && time > maxTime) 
        { 
            GameObject _bullet = Instantiate(bullet);
            target = GetComponent<Enemy>().GetTarget();
            _bullet.transform.position = transform.position;
            _bullet.transform.SetParent(transform);
            _bullet.GetComponent<Rigidbody2D>().velocity = (target.transform.localPosition - transform.localPosition).normalized * speed;
            time = 0;
        }
    }
}
