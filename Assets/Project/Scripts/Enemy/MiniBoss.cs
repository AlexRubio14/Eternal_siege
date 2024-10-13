using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Enemy
{
    [Header("SmashAttack")]
    [SerializeField] private float cd;
    [SerializeField] private float attackCircleDamage;
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

    private void OnTriggerStay(Collider collision)
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
            GameObject attackArea = Instantiate(attackCircle, transform);
            attackArea.transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);
            animator.SetBool("Attack", true);
            canMove = false;
            startAttack = true;
            Invoke("Attack", animator.GetCurrentAnimatorStateInfo(0).length * 1.5f);
        }
    }

    private void Attack()
    {
        transform.GetChild(1).gameObject.GetComponent<SphereCollider>().enabled = true;
        timeCd = cd;
        animator.SetBool("Attack", false);
        canMove = true;
        startAttack = false;
        for(int i = 0; i<PlayersManager.instance.GetPlayersList().Count; i++) 
        {
            if (PlayersManager.instance.GetPlayersList()[i].GetComponent<PlayerController>().GetIsInArea())
            {
                PlayersManager.instance.GetPlayersList()[i].GetComponent<PlayerController>().ReceiveDamage(attackCircleDamage);
            }
        }
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
            transform.GetChild(1).transform.GetChild(0).transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 1.75f);
        }
    }

}
