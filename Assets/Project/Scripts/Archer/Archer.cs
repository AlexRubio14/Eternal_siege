using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Archer : Character
{
    [SerializeField] protected GameObject arrowPrefab;
    [SerializeField] protected GameObject abilityPrefab;
    [SerializeField] protected GameObject ultimatePrefab;

    [SerializeField] protected Transform firingPoint;

    private void Start()
    {
        base.Start();
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
            Instantiate(abilityPrefab, firingPoint.position, Quaternion.identity);            
            abilityTimer = abilityCooldown;
            UIManager.instance.InitTimer(false, this);
        }
    }

    protected override void UltimateAbility()
    {
        if (ultimateTimer <= 0f)
        {
            Instantiate(ultimatePrefab, firingPoint.position, firingPoint.rotation);
            ultimateTimer = ultimateCooldown;
            UIManager.instance.InitTimer(true, this);
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
}
