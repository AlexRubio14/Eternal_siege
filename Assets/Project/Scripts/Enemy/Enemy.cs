using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameObject[] target = new GameObject[2];
    private Rigidbody2D rgbd2d;

    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target[0] != null) 
        {
            Vector3 direction = (target[0].transform.localPosition - transform.localPosition).normalized;
            for(int i = 1; i < target.Length; i++) 
            {
                float distancePostion1 = Vector3.Distance(transform.localPosition, target[i].transform.position);
                float distancePostion2 = Vector3.Distance(transform.localPosition, target[i - 1].transform.position);
                if (distancePostion1 < distancePostion2)
                {
                    direction = (target[i].transform.localPosition - transform.localPosition).normalized; ;
                }
            }
            rgbd2d.velocity = direction * speed;
        }


    }

    public void SetTarget(GameObject[] _target)
    {
        for (int i = 0; i < _target.Length; i++)
        {
            target[i] = _target[i];
        }
    }
}
