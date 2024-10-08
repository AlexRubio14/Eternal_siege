using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Archer : Character
{
    [SerializeField] protected GameObject arrowPrefab;
    [SerializeField] protected GameObject ultimatePrefab;

    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected Transform firingPointLeft;
    [SerializeField] protected Transform firingPointRight;

    [SerializeField] private float abilitySpeedMultiplier;
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected float attackSpeedAugment;
    protected bool doubleShooting;

    private void Start()
    {
        base.Start();
        playerController.SetSpeed(movementSpeed);
    }

    protected void Update()
    {
        base.Update();
    }

    #region ATTACKS & ABILITIES
    protected override void BasicAttack() 
    {
        Instantiate(arrowPrefab, firingPoint.position, Quaternion.identity);
    }

    protected override void BasicAbility()
    {
        if (playerController.GetCurrentState() == PlayerController.State.KNOCKBACK || playerController.GetCurrentState() == PlayerController.State.DEAD)
            return;

        if (abilityTimer <= 0f)
        {
            attackSpeed += attackSpeed * attackSpeedAugment;

            doubleShooting = true;
            movementSpeed *= abilitySpeedMultiplier;
            playerController.SetSpeed(movementSpeed);

            abilityTimer = abilityCooldown + abilityDuration;
        }
    }

    protected override void UltimateAbility()
    {
        if (ultimateTimer <= 0f)
        {
            Instantiate(ultimatePrefab, firingPoint.position, firingPoint.rotation);
            ultimateTimer = ultimateCooldown;
        }
    }
    #endregion

    #region UPDATE TIMERS
    protected override void UpdateFireTimer()
    {
        if (playerController.GetCurrentState() == PlayerController.State.DEAD || playerController.GetCurrentState() == PlayerController.State.KNOCKBACK)
            return;

        if (fireTimer <= 0f)
        {
            if (doubleShooting)
                DoubleShoot();
            else
                BasicAttack();

            fireTimer = 1 / attackSpeed;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    protected override void UpdateAbilityTimer()
    {
        if (abilityTimer > 0f)
        {
            abilityTimer -= Time.deltaTime;
            if (abilityTimer < abilityCooldown)
            {
                attackSpeed = baseAttackSpeed;
                doubleShooting = false;
                movementSpeed = baseMovementSpeed;
                playerController.SetSpeed(movementSpeed);
            }
        }
    }

    protected override void UpdateUltimateTimer()
    {
        if (ultimateTimer > 0f)
        {
            ultimateTimer -= Time.deltaTime;
        }
    }
    #endregion

    protected void DoubleShoot()
    {
        Instantiate(arrowPrefab, firingPointLeft.position, Quaternion.identity);
        Instantiate(arrowPrefab, firingPointRight.position, Quaternion.identity);
    }
}
