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

    private void Start()
    {
        baseAttackSpeed = attackSpeed;
    }

    private void Update()
    {
        UpdateFireTimer();
        UpdateAbilityTimer();
        UpdateUltimateTimer();

        //Check Inputs
        if (Input.GetMouseButtonDown(0) && abilityTimer <= 0f)
            Ability();
        if (Input.GetMouseButtonDown(1) && ultimateTimer <= 0f)
            Ultimate();
    }

    private void Shoot()
    {
        Instantiate(arrowPrefab, firingPoint.position, firingPoint.rotation);
    }

    private void DoubleShoot()
    {
        Instantiate(arrowPrefab, firingPointLeft.position, firingPointLeft.rotation);
        Instantiate(arrowPrefab, firingPointRight.position, firingPointRight.rotation);
    }

    private void Ability()
    {
        attackSpeed += attackSpeed * attackSpeedAugment;

        doubleShooting = true;
        //Falta movementSpeed

        abilityTimer = abilityCooldown + abilityDuration;
    }

    private void Ultimate()
    {
        Instantiate(ultimatePrefab, firingPoint.position, firingPoint.rotation);
        ultimateTimer = ultimateCooldown;
    }

    private void UpdateFireTimer()
    {
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
