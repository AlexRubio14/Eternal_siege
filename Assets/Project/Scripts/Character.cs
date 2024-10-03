using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float attackSpeed;
    protected float fireTimer;

    [SerializeField] protected float abilityCooldown;
    protected float abilityTimer;

    [SerializeField] protected float ultimateCooldown;
    protected float ultimateTimer;

    protected abstract void BasicAttack();
    protected abstract void BasicAbility();
    protected abstract void UltimateAbility();

    protected abstract void UpdateFireTimer();
    protected abstract void UpdateAbilityTimer();
    protected abstract void UpdateUltimateTimer();

    protected void Update()
    {
        UpdateFireTimer();
        UpdateAbilityTimer();
        UpdateUltimateTimer();
    }

    public void BasicAbilityAction(InputAction.CallbackContext obj)
    {
        BasicAbility();
    }

    public void UltimateAbilityAction(InputAction.CallbackContext obj)
    {
        UltimateAbility();
    }


}