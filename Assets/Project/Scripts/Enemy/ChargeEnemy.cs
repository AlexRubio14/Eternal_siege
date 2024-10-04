using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChargeEnemy : Enemy
{
    [Header("Charge")]
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float distanceSeparation;
    [SerializeField] private float maxTime;
    private enum enemyState { Charging, Recovery, Seek };
    private enemyState currentState;
    private bool canCharge;
    private bool chargeStarted;

    private Vector2 startPosition;
    private float targetPosition;
    private float time;

    private void Awake()
    {
        Initialize();
        canCharge = true;
        chargeStarted = false;
        currentState = enemyState.Seek;
    }
    protected override void Update()
    {
        switch(currentState)
        {
            case enemyState.Seek:
                base.Update();
                break;
            case enemyState.Charging:
                FinishDistance();
                break;

            case enemyState.Recovery:
                base.Update();
                Recovery();
                break;
        }
    }

    private void FinishDistance()
    {
        if (Vector2.Distance(startPosition, transform.localPosition) >= targetPosition && chargeStarted)
        {
            EndCharging();
            chargeStarted = false;
            animator.SetBool("Running", false);
        }
    }

    private void Recovery()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            canCharge = true;
            currentState = enemyState.Seek;
        }
    }
    private void Charge()
    {
        animator.SetBool("Screaming", false);
        animator.SetBool("Running", true);
        speed *= speedMultiplier;
        direction *= 2f;
        startPosition = transform.localPosition;
        targetPosition = Vector2.Distance(currentTarget.transform.localPosition, transform.localPosition) * distanceSeparation;
        rgbd2d.velocity = speed * direction;
        chargeStarted = true;
    }

    private void PrepareCharging()
    {
        animator.SetBool("Screaming", true);
        rgbd2d.velocity = Vector3.zero;
        Invoke("Charge", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void EndCharging()
    {
        speed /= speedMultiplier;
        currentState = enemyState.Recovery;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canCharge)
        {
            canCharge = false;
            currentState = enemyState.Charging;
            PrepareCharging();
        }
    }
}
