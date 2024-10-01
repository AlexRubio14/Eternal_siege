using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [SerializeField] private float cd;
    [SerializeField] private GameObject attackCircle;

    private GameObject currentTarget;
    private Animator animator;
    private bool targetDetected;
    private bool startAttack;
    private float timeCd;

    private void Start()
    {
        currentTarget = GetComponent<Enemy>().GetCurrentTarget();
        animator = GetComponent<Animator>();
        targetDetected = false;
        startAttack = false;
        timeCd = 0;
    }

    private void Update()
    {
        UpdateTarget();
        Attack();
        PrepateAttack();
    }

    private void Attack()
    {
        if (startAttack)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            transform.GetChild(0).transform.GetChild(0).transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, stateInfo.normalizedTime);
            if (stateInfo.normalizedTime > 1.0f)
            {              
                timeCd = cd;
                animator.SetBool("Attack", false);
                GetComponent<Enemy>().SetCanMove(true);
                startAttack = false;
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

    private void PrepateAttack()
    {
        if (timeCd <= 0 && targetDetected && !startAttack)
        {
            Instantiate(attackCircle, transform);
            GetComponent<Enemy>().SetCanMove(false);
            animator.SetBool("Attack", true);
            startAttack = true;
        }
        else if (timeCd > 0)
        {
            timeCd -= Time.deltaTime;
        }
    }

    private void UpdateTarget()
    {
        if (currentTarget != GetComponent<Enemy>().GetCurrentTarget())
        {
            currentTarget = GetComponent<Enemy>().GetCurrentTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject == currentTarget)
        {
            targetDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject == currentTarget)
        {
            targetDetected = false;
        }
    }

}
