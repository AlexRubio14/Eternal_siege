using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [SerializeField] private float cd;
    [SerializeField] private GameObject attackCircle;

    private GameObject currentTarget;
    private Animator animator;
    private bool startAttack;
    private float timeCd;

    private void Start()
    {
        currentTarget = GetComponent<Enemy>().GetCurrentTarget();
        animator = GetComponent<Animator>();
        startAttack = false;
        timeCd = 0;
    }

    private void Update()
    {
        UpdateTarget();

        if (timeCd > 0)
        {
            timeCd -= Time.deltaTime;
        }
        if(startAttack)
        {
            transform.GetChild(0).transform.GetChild(0).transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
    }

    private void Attack()
    {
        timeCd = cd;
        animator.SetBool("Attack", false);
        GetComponent<Enemy>().SetCanMove(true);
        startAttack = false;
        Destroy(transform.GetChild(0).gameObject);
    }

    private void PrepareAttack()
    {
        if (timeCd <= 0 && !startAttack)
        {
            Instantiate(attackCircle, transform);
            GetComponent<Enemy>().SetCanMove(false);
            animator.SetBool("Attack", true);
            startAttack = true;
            Invoke("Attack", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void UpdateTarget()
    {
        if (currentTarget != GetComponent<Enemy>().GetCurrentTarget())
        {
            currentTarget = GetComponent<Enemy>().GetCurrentTarget();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject == currentTarget)
        {
            PrepareAttack();
        }
    }

}
