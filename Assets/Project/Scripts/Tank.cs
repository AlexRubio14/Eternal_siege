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
    [SerializeField] private float abilitySpeedMultiplier;
    [SerializeField] private float ultimateSpeedMultiplier;
    private float interpolationTime;
    private bool isAbiltyActive;

    [SerializeField] private float ultimateDamage;
    private bool isUltimateActive;


    private void Start()
    {
        base.Start();
        DisableAttackCollider();
        interpolationTime = 0f;
        isAbiltyActive = false;
        isUltimateActive = false;
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
        if (!isUltimateActive) 
        {
            isAbiltyActive = true;
            interpolationTime = 0f;
            movementSpeed *= abilitySpeedMultiplier;
            playerController.SetSpeed(movementSpeed);
            abilityTimer = abilityCooldown + abilityDuration;
        }
    }

    protected override void UltimateAbility()
    {
        movementSpeed *= abilitySpeedMultiplier;
        playerController.SetSpeed(movementSpeed);
        playerController.ChangeState(PlayerController.State.INVENCIBILITY);
        abilityTimer = abilityCooldown; //cancelar BasicAbility
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
                movementSpeed = baseMovementSpeed;
                playerController.SetSpeed(movementSpeed);
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
                movementSpeed = baseMovementSpeed;
                playerController.SetSpeed(movementSpeed);
            }
        }
    }
    #endregion

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isUltimateActive)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(ultimateDamage);
        }
    }
}
