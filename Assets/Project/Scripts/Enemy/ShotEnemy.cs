using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float maxTime;
    [SerializeField] private float speed;

    private float time;
    private GameObject target;

    private void Start()
    {
        time = maxTime;
    }
    private void Update()
    {
        target = GetComponent<Enemy>().GetTarget();
        time += Time.deltaTime;
        if (time > maxTime && target != null) 
        {
            GameObject _bullet = Instantiate(bullet);
            _bullet.transform.position = transform.position;
            _bullet.transform.SetParent(transform);
            _bullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.localPosition).normalized * speed;
            time = 0;
        }
    }
}
