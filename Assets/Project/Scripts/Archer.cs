using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject ultimatePrefab;

    [SerializeField] private Transform firingPoint;
    [SerializeField] private Transform firingPointLeft;
    [SerializeField] private Transform firingPointRight;

    [Range(0.1f, 5f)]
    [SerializeField] private float attackSpeed;
    private float baseAttackSpeed;
    private float fireTimer;

    [SerializeField] private float abilityCooldown;
    private float abilityTimer;
    [SerializeField] private float abilityDuration;
    [SerializeField] private float attackSpeedAugment;

    [SerializeField] private float ultimateCooldown;
    private float ultimateTimer;

    private bool doubleShooting;

    [SerializeField] PlayerController playerController;

    private void Start()
    {
        baseAttackSpeed = attackSpeed;
    }

    private void Update()
    {
        UpdateFireTimer();
        UpdateAbilityTimer();
        UpdateUltimateTimer();
    }

    private void Shoot()
    {
        Instantiate(arrowPrefab, firingPoint.position, Quaternion.identity);
    }

    private void DoubleShoot()
    {
        Instantiate(arrowPrefab, firingPointLeft.position, Quaternion.identity);
        Instantiate(arrowPrefab, firingPointRight.position, Quaternion.identity);
    }

    public void Ability()
    {
        if (abilityTimer <= 0f)
        {
            attackSpeed += attackSpeed * attackSpeedAugment;

            doubleShooting = true;
            //Falta movementSpeed

            abilityTimer = abilityCooldown + abilityDuration;
        }
    }

    public void Ultimate()
    {
        if (ultimateTimer <= 0f)
        {
            Instantiate(ultimatePrefab, firingPoint.position, firingPoint.rotation);
            ultimateTimer = ultimateCooldown;
        }
    }

    private void UpdateFireTimer()
    {
        if (playerController.GetCurrentState() == PlayerController.State.DEAD || playerController.GetCurrentState() == PlayerController.State.KNOCKBACK)
            return;

        if (fireTimer <= 0f)
        {
            if (doubleShooting)
                DoubleShoot();
            else
                Shoot();

            fireTimer = 1 / attackSpeed;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void UpdateAbilityTimer()
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

    private void UpdateUltimateTimer()
    {
        if (ultimateTimer > 0f)
        {
            ultimateTimer -= Time.deltaTime;
        }
    }


}
