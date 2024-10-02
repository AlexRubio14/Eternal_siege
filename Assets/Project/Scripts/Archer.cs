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

    protected float baseAttackSpeed;
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected float attackSpeedAugment;
    protected bool doubleShooting;

    protected void Start()
    {
        baseAttackSpeed = attackSpeed;
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
        if (abilityTimer <= 0f)
        {
            attackSpeed += attackSpeed * attackSpeedAugment;

            doubleShooting = true;
            //Falta movementSpeed

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
