using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChargeEnemy : Enemy
{
    [Header("Charge")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTimeCharging;
    [SerializeField] private float maxTime;
    private enum enemyState { Charging, Recovery, Seek, Screaming };
    private enemyState currentState;
    private bool canCharge;
    private bool chargeStarted;

    private float time;
    private float timeStopCharging;

    private void Awake()
    {
        Initialize();
        canCharge = true;
        chargeStarted = false;
        currentState = enemyState.Seek;
        timeStopCharging = 0;
    }
    protected override void Update()
    {
        switch(currentState)
        {
            case enemyState.Seek:
                base.Update();
                break;
            case enemyState.Charging:
                rgbd2d.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Force);
                FinishDistance();
                break;

            case enemyState.Recovery:
                base.Update();
                Recovery();
                break;
            case enemyState.Screaming:
                break;
        }
    }

    private void FinishDistance()
    {
        timeStopCharging += Time.deltaTime;
        if (timeStopCharging > maxTimeCharging && chargeStarted)
        {
            chargeStarted = false;
            animator.SetBool("Running", false);
            timeStopCharging = 0;
            EndCharging();
        }
    }

    private void Recovery()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            canCharge = true;
            currentState = enemyState.Seek;
            time = 0;
        }
    }
    private void Charge()
    {  
        animator.SetBool("Screaming", false);
        animator.SetBool("Running", true);
        direction *= 2.0f;
        speed *= maxSpeed;
        chargeStarted = true;
        currentState = enemyState.Charging;
    }

    private void PrepareCharging()
    {
        animator.SetBool("Screaming", true);
        rgbd2d.velocity = Vector3.zero;
        Invoke("Charge", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void EndCharging()
    {
        rgbd2d.velocity = Vector3.zero;
        speed /= maxSpeed;
        currentState = enemyState.Recovery;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canCharge)
        {
            canCharge = false;
            currentState = enemyState.Screaming;
            PrepareCharging();
        }
        if(collision.CompareTag("Player") && currentState == enemyState.Charging && collision is BoxCollider2D)
        {
            chargeStarted = false;
            animator.SetBool("Running", false);
            timeStopCharging = 0;
            EndCharging();
        }

    }
}
