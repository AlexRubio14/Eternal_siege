using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D attackCollider;
    [SerializeField] private float attackSpeed;
    private float fireTimer;

    [SerializeField] private float abilityCooldown;
    private float abilityTimer;
    [SerializeField] private float abilityDuration;
    [SerializeField] private CircleCollider2D abilityCollider;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    private float interpolationTime;
    private bool isAbiltyActive;

    [SerializeField] private float ultimateCooldown;
    private float ultimateTimer;

    private void Start()
    {
        DisableAttackCollider();
        interpolationTime = 0f;
        isAbiltyActive = false;
    }

    private void Update()
    {
        UpdateFireTimer();
        UpdateAbilityTimer();
        UpdateUltimateTimer();

        if (isAbiltyActive)
        {
            if (interpolationTime < 1f) 
            {
                interpolationTime += Time.deltaTime;
            }
            abilityCollider.radius = Mathf.Lerp(minRadius, maxRadius, interpolationTime);
        }
        else
        {
            if (interpolationTime < 1f)
            {
                interpolationTime += Time.deltaTime;
            }
            abilityCollider.radius = Mathf.Lerp(maxRadius, minRadius, interpolationTime);
        }

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
        isAbiltyActive = true;
        interpolationTime = 0f;
        //Falta decrecer movementSpeed
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
                isAbiltyActive = false;
                interpolationTime = 0f;
            }
        }
    }

    private void UpdateUltimateTimer()
    {
        if (ultimateTimer > 0f)
        {
            ultimateTimer -= Time.deltaTime;
            if (ultimateTimer < ultimateCooldown)
            {
                //activar invulnerabilidad
                //incrementar movement speed
                //bloquear activar habilidad
            }
        }
    }
}
