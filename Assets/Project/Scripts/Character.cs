using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float attackSpeed;
    protected float baseAttackSpeed;
    protected float fireTimer;

    [SerializeField] protected float abilityCooldown;
    protected float abilityTimer;

    [SerializeField] protected float ultimateCooldown;
    protected float ultimateTimer;

    [SerializeField] protected float movementSpeed;
    protected float baseMovementSpeed;

    [SerializeField] protected float cooldown;
    [SerializeField] protected float health;
    [SerializeField] protected float healthRegen;
    [SerializeField] protected float armor;
    [SerializeField] protected float pickUpRadius;
    protected float damageMultiplier;


    [SerializeField] protected PlayerController playerController;

    protected abstract void BasicAttack();
    protected abstract void BasicAbility();
    protected abstract void UltimateAbility();

    protected abstract void UpdateFireTimer();
    protected abstract void UpdateAbilityTimer();
    protected abstract void UpdateUltimateTimer();

    protected void Start()
    {
        baseAttackSpeed = attackSpeed;
        baseMovementSpeed = movementSpeed;
        damageMultiplier = 1f;

        playerController.SetSpeed(movementSpeed);
        playerController.SetMaxHealth(health);
    }

    protected void Update()
    {
        UpdateFireTimer();
        UpdateAbilityTimer();
        UpdateUltimateTimer();
    }

    public void BasicAbilityAction(InputAction.CallbackContext obj)
    {
        if (playerController.GetCurrentState() == PlayerController.State.KNOCKBACK || playerController.GetCurrentState() == PlayerController.State.DEAD)
            return; 

        BasicAbility();
    }

    public void UltimateAbilityAction(InputAction.CallbackContext obj)
    {
        if (playerController.GetCurrentState() == PlayerController.State.KNOCKBACK || playerController.GetCurrentState() == PlayerController.State.DEAD)
            return;

        UltimateAbility();
    }


}
