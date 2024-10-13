using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShotEnemy : Enemy
{
    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxTime;
    private float time;

    private void Awake()
    {
        Initialize();
        time = maxTime;
    }
    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
    }

    private void CheckIfCanShoot()
    {
        if (time > maxTime && target != null)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.identity);
        _bullet.GetComponent<Rigidbody>().velocity = (currentTarget.transform.position - transform.localPosition).normalized * bulletSpeed * Time.deltaTime;
        time = 0;
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Player") && canMove)
        {
            CheckIfCanShoot();
        }
    }

}
