using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueliteManager : MonoBehaviour
{
    public static RogueliteManager instance;

    private float armor;
    private float hpMultiplier;
    private float hpRegen;
    private float damageMultiplier;
    private float cooldownMultiplier;
    private float attackSpeedMultiplier;
    private float movementSpeedMultiplier;
    private float experienceMultiplier;
    private float pickUpRadiusMultiplier;

    [SerializeField] private float increaseArmor;
    [Range(1f, 2f)]
    [SerializeField] private float increaseHpMultiplier;
    [SerializeField] private float increaseHpRegen;
    [Range(1f, 2f)]
    [SerializeField] private float increaseDamageMultiplier;
    [Range(1f, 2f)]
    [SerializeField] private float increaseCooldownMultiplier;
    [Range(1f, 2f)]
    [SerializeField] private float increaseAttackSpeedMultiplier;
    [Range(1f, 2f)]
    [SerializeField] private float increaseMovementSpeedMultiplier;
    [Range(1f, 2f)]
    [SerializeField] private float increaseExperienceMultiplier;
    [Range(1f, 2f)]
    [SerializeField] private float increasePickUpRadiusMultiplier;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        ResetUpgrades();
    }

    private void ResetUpgrades()
    {
        armor = 0f;
        hpMultiplier = 1f;
        hpRegen = 0f;
        damageMultiplier = 1f;
        cooldownMultiplier = 1f;
        attackSpeedMultiplier = 1f;
        movementSpeedMultiplier = 1f;
        experienceMultiplier = 1f;
        pickUpRadiusMultiplier = 1f;
    }

    #region GETTERS
    public float GetArmorSum()
    {
        return armor;
    }
    public float GetHpMultiplier()
    {
        return hpMultiplier;
    }
    public float GetHpRegenSum()
    {
        return hpRegen;
    }
    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
    public float GetCooldownMultiplier()
    {
        return cooldownMultiplier;
    }
    public float GetAttackSpeedMultiplier()
    {
        return attackSpeedMultiplier;
    }
    public float GetMovementSpeedMultiplier()
    {
        return movementSpeedMultiplier;
    }
    public float GetExperienceMultiplier()
    {
        return experienceMultiplier;
    }
    public float GetPickUpRadiusMultiplier()
    {
        return pickUpRadiusMultiplier;
    }
    #endregion

    #region SETTERS
    public void IncreaseArmorSum()
    {
        armor += increaseArmor;
    }
    public void IncreaseHpMultiplier()
    {
        hpMultiplier += increaseHpMultiplier;
    }
    public void IncreaseHpRegenSum()
    {
        hpRegen += increaseHpRegen;
    }
    public void IncreaseDamageMultiplier()
    {
        damageMultiplier += increaseDamageMultiplier;
    }
    public void IncreaseCooldownMultiplier()
    {
        cooldownMultiplier += increaseCooldownMultiplier;
    }
    public void IncreaseAttackSpeedMultiplier()
    {
        attackSpeedMultiplier += increaseAttackSpeedMultiplier;
    }
    public void IncreaseMovementSpeedMultiplier()
    {
        movementSpeedMultiplier += increaseMovementSpeedMultiplier;
    }
    public void IncreaseExperienceMultiplier()
    {
        experienceMultiplier += increaseExperienceMultiplier;
    }
    public void IncreasePickUpRadiusMultiplier()
    {
        pickUpRadiusMultiplier += increasePickUpRadiusMultiplier;
    }
    #endregion
}
