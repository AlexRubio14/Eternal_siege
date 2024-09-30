using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private List<GameObject> target;
    private GameObject currentTarget;
    private Rigidbody2D rgbd2d;
    
    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target.Count > 0 && target[0]) 
        {
            Vector3 direction = (target[0].transform.localPosition - transform.localPosition).normalized;
            currentTarget = target[0];
            for (int i = 1; i < target.Count; i++) 
            {
                float distancePostion1 = Vector3.Distance(transform.localPosition, target[i].transform.position);
                float distancePostion2 = Vector3.Distance(transform.localPosition, target[i - 1].transform.position);
                if (distancePostion1 < distancePostion2)
                {
                    direction = (target[i].transform.localPosition - transform.localPosition).normalized;
                    currentTarget = target[i];
                }
            }
            rgbd2d.velocity = direction * speed;
        }
    }

    public void SetTarget(List<GameObject> _target)
    {
        target = _target;
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }
}
