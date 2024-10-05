using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Enemy
{
    [Header("SmashAttack")]
    [SerializeField] private float cd;
    [SerializeField] private GameObject attackCircle;
    private bool startAttack;
    private float timeCd;

    private void Awake()
    {
        Initialize();

        animator = GetComponent<Animator>();
        startAttack = false;
        timeCd = 0;
    }

    protected override void Update()
    {
        base.Update();

        SmashCd();
        CircleLerp();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject == currentTarget)
        {
            PrepareAttack();
        }
    }

    private void PrepareAttack()
    {
        if (timeCd <= 0 && !startAttack)
        {
            Instantiate(attackCircle, transform);
            animator.SetBool("Attack", true);
            canMove = false;
            startAttack = true;
            Invoke("Attack", animator.GetCurrentAnimatorStateInfo(0).length * 2);
        }
    }

    private void Attack()
    {
        timeCd = cd;
        animator.SetBool("Attack", false);
        canMove = true;
        startAttack = false;
        Destroy(transform.GetChild(1).gameObject);
    }

    private void SmashCd()
    {
        if (timeCd > 0)
        {
            timeCd -= Time.deltaTime;
        }

        if(currentHP <= 0 && transform.GetChild(1) != null)
        {
            Destroy(transform.GetChild(1));
        }

    }

    private void CircleLerp()
    {
        if (startAttack)
        {
            transform.GetChild(1).transform.GetChild(0).transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 2);
        }
    }
}
