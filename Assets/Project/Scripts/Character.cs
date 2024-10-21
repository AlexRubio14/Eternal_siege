using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float armor;
    [SerializeField] protected float health;
    protected float healthRegen;
    protected float damageMultiplier;
    protected float cooldown;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float pickUpRadius;

    protected float baseAttackSpeed;
    protected float fireTimer;

    protected float baseMovementSpeed;

    [SerializeField] protected float abilityCooldown;
    protected float abilityTimer;
    [SerializeField] protected float ultimateCooldown;
    protected float ultimateTimer;

    private bool magicCapeSpawned;


    [SerializeField] protected PlayerController playerController;

    protected abstract void BasicAttack();
    protected abstract void BasicAbility();
    protected abstract void UltimateAbility();

    protected abstract void UpdateFireTimer();
    protected abstract void UpdateAbilityTimer();
    protected abstract void UpdateUltimateTimer();

    protected void Start()
    {
        InitStats();
        magicCapeSpawned = false;
    }

    private void InitStats()
    {
        armor += RogueliteManager.instance.GetArmorSum();
        health *= RogueliteManager.instance.GetHpMultiplier();
        healthRegen += RogueliteManager.instance.GetHpRegenSum();
        damageMultiplier = RogueliteManager.instance.GetDamageMultiplier();
        cooldown = RogueliteManager.instance.GetCooldownMultiplier();
        attackSpeed *= RogueliteManager.instance.GetAttackSpeedMultiplier();
        movementSpeed *= RogueliteManager.instance.GetMovementSpeedMultiplier();
        pickUpRadius *= RogueliteManager.instance.GetPickUpRadiusMultiplier();

        abilityCooldown -= abilityCooldown * cooldown - abilityCooldown;
        ultimateCooldown -= ultimateCooldown * cooldown - ultimateCooldown;

        baseAttackSpeed = attackSpeed;
        baseMovementSpeed = movementSpeed;
        damageMultiplier = 1f;

        playerController.SetSpeed(movementSpeed);
        playerController.SetMaxHealth(health);
    }

    protected void Update()
    {
        if(TimeManager.instance.GetPaused() == 0) 
            return;

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

    public void AddHealth(float quantity)
    {
        playerController.SetMaxHealth(health + quantity);
    }

    public void AddArmor(float quantity)
    {
        armor += quantity;
    }

    public void AddSpeed(float quantity)
    {
        playerController.SetSpeed(playerController.GetSpeed() + quantity);
    }

    public void AddAttackSpeed(float quantity)
    {
        baseAttackSpeed += quantity;
        attackSpeed += quantity;
    }

    public void SetMagicCapeSpawned(bool state)
    {
        magicCapeSpawned = state;
    }

    public bool GetMagicCapeSpawned()
    {
        return magicCapeSpawned;
    }
}
