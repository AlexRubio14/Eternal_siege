using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public abstract class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float speed;
    protected Vector2 direction;
    protected bool canMove;
    protected Rigidbody2D rgbd2d;

    [Header("Combat")]
    [SerializeField] protected int maxHP;
    [SerializeField] protected float damage;
    [SerializeField] protected float experience;
    [SerializeField] protected GameObject experienceBall;
    protected float currentHP;
    protected List<GameObject> target;
    protected GameObject currentTarget;

    [Header("Animation")]
    [SerializeField] protected float animationDivide;
    protected Animator animator;

    protected void Initialize()
    {
        animator = GetComponent<Animator>();
        rgbd2d = GetComponent<Rigidbody2D>();

        currentHP = maxHP;
        canMove = true;
    }

    protected virtual void Update()
    {
        Seek();
    }
    private void Seek()
    {
        if (target.Count > 0 && canMove)
        {
            direction = (Vector2)target[0].transform.localPosition - (Vector2)transform.localPosition;
            currentTarget = target[0];
            for (int i = 1; i < target.Count; i++)
            {
                Vector2 distancePostion1 = (Vector2)target[i].transform.localPosition - (Vector2)transform.localPosition;
                if (distancePostion1.magnitude < direction.magnitude)
                {
                    direction = distancePostion1;
                    currentTarget = target[i];
                }
            }
            rgbd2d.velocity = direction.normalized * speed;
        }
        if (!canMove)
        {
            rgbd2d.velocity = Vector3.zero;
        }
        Rotate();
    }

    private void Rotate()
    {
        transform.up = direction;
    }

    public void ReceiveDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            animator.SetBool("Die", true);
            currentHP = 0;
            canMove = false;
            rgbd2d.velocity = Vector3.zero;
            Invoke("Die", animator.GetCurrentAnimatorStateInfo(0).length / animationDivide);
        }
    }
    private void Die()
    {
        GameObject _experienceBall = Instantiate(experienceBall, transform.localPosition, Quaternion.identity);
        _experienceBall.GetComponent<ExperienceBall>().SetExperience(experience);
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<BoxCollider2D>().enabled = false;
        EnemyManager.instance.GetEnemies().Remove(gameObject);
        Destroy(gameObject);
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

    public float GetDamage()
    {
        return damage;
    }
}
