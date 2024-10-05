using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Character
{
    [SerializeField] private CapsuleCollider2D attackCollider;

    [SerializeField] private float abilityDuration;
    [SerializeField] private CircleCollider2D abilityCollider;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    private float interpolationTime;
    private bool isAbiltyActive;


    private void Start()
    {
        DisableAttackCollider();
        interpolationTime = 0f;
        isAbiltyActive = false;
    }

    private void Update()
    {
        base.Update();

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

        //if (Input.GetMouseButtonDown(0) && abilityTimer <= 0f)
        //    Ability();
        //if (Input.GetMouseButtonDown(1) && ultimateTimer <= 0f)
        //    Ultimate();
    }

    #region ATTACKS & ABILITIES
    protected override void BasicAttack()
    {
        attackCollider.enabled = true;
        Invoke("DisableAttackCollider", 0.1f);
        Invoke("BasicAttack", 0.3f);
    }
    protected override void BasicAbility()
    {
        isAbiltyActive = true;
        interpolationTime = 0f;
        //Falta decrecer movementSpeed
        abilityTimer = abilityCooldown + abilityDuration;
    }

    protected override void UltimateAbility()
    {
        //aumentar move speed
        //volverse invulnerable
        //cancelar BasicAbility
        //hacer daño al chocar con otros enemigos
        ultimateTimer = ultimateCooldown;
    }
    #endregion

    #region UPDATE TIMERS
    protected override void UpdateFireTimer()
    {
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
            if (abilityTimer < abilityCooldown)
            {
                isAbiltyActive = false;
                interpolationTime = 0f;
            }
        }
    }

    protected override void UpdateUltimateTimer()
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
    #endregion

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }
}
