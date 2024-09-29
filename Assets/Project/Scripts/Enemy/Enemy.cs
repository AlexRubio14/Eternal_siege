using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameObject[] target = new GameObject[2];
    private GameObject currentTarget;
    private Rigidbody2D rgbd2d;
    private bool canMove;

    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    private void Update()
    {
        if (target[0] != null && canMove) 
        {
            Vector3 direction = (target[0].transform.localPosition - transform.localPosition).normalized;
            currentTarget = target[0];
            for (int i = 1; i < target.Length; i++) 
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
        else if(!canMove)
        {
            rgbd2d.velocity = Vector3.zero;
        }


    }

    public void SetTarget(GameObject[] _target)
    {
        for (int i = 0; i < _target.Length; i++)
        {
            target[i] = _target[i];
        }
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }
}
