using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private float attackSpeed;
    private float fireTimer;

    [SerializeField] private float abilityCooldown;
    private float abilityTimer;
    [SerializeField] private float abilityDuration;

    [SerializeField] private float ultimateCooldown;
    private float ultimateTimer;

    private void Start()
    {
        DisableAttackCollider();
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
        attackCollider.enabled = true;
        Invoke("DisableAttackCollider", 0.5f);
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }

    private void Ability()
    {
        //aumentar radio de el circulo en un periodo de tiempo corto
        //Falta movementSpeed

        abilityTimer = abilityCooldown + abilityDuration;
    }

    private void Ultimate()
    {
        //Pensar en algo
        ultimateTimer = ultimateCooldown;
    }

    private void UpdateFireTimer()
    {
        if (fireTimer <= 0f)
        {
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
                //decrecer collider
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
