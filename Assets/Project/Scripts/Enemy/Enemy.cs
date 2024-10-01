using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHP;
    [SerializeField] private float experience;
    [SerializeField] private GameObject experienceBall;

    private List<GameObject> target;
    private GameObject currentTarget;
    private Rigidbody2D rgbd2d;
    private Animator animator;
    private float currentHP;
    private bool isDead;
    private bool canMove;
    Vector3 direction;


    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHP = maxHP;
        isDead = false;
        canMove = true;
    }

    private void Update()
    {
        Seek();
        Die();
    }
    private void Seek()
    {

        if (target.Count > 0 && target[0] && !isDead && canMove)
        {
            direction = (target[0].transform.localPosition - transform.localPosition).normalized;
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
        if(!canMove)
        {
            rgbd2d.velocity = Vector3.zero;
        }
        Rotate();
    }

    private void Die()
    {
        if (isDead)
        {
            animator.SetBool("Die", true);

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime > 1.0f)
            {
                GameObject _experienceBall = Instantiate(experienceBall);
                _experienceBall.transform.localPosition = transform.localPosition;
                _experienceBall.GetComponent<ExperienceBall>().SetExperience(experience);
                Destroy(gameObject);
            }
        }
    }

    private void Rotate()
    {
        if (rgbd2d.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rgbd2d.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void ReciveDamage(float amount)
    {
        currentHP -= amount;
        if(currentHP < 0)
        {
            currentHP = 0;
            rgbd2d.velocity = Vector3.zero;
            isDead = true;
        }
    }

    public void SetTarget(List<GameObject> _target)
    {
        target = _target;
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }
}
