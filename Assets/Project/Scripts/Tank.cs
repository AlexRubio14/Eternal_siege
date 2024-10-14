using UnityEngine;

public class Tank : Character
{
    [SerializeField] private CapsuleCollider attackCollider;
    [SerializeField] private MeshRenderer attackMeshRenderer;

    [SerializeField] private float abilityDuration;
    [SerializeField] private GameObject abilityCollider;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float abilitySpeedMultiplier;
    private float interpolationTime;
    private bool isAbiltyActive;

    [SerializeField] private float ultimateDuration;
    [SerializeField] private float ultimateSpeedMultiplier;
    [SerializeField] private float ultimateDamage;
    private bool isUltimateActive;

    private void Start()
    {
        base.Start();
        DisableAttackCollider();
        interpolationTime = 1f;
        isAbiltyActive = false;
        isUltimateActive = false;
    }

    private void Update()
    {
        base.Update();
        if (isAbiltyActive && interpolationTime < 1f)
        {
            interpolationTime += Time.deltaTime;
            abilityCollider.transform.localScale = new Vector3(Mathf.Lerp(minScale, maxScale, interpolationTime), Mathf.Lerp(minScale, maxScale, interpolationTime), Mathf.Lerp(minScale, maxScale, interpolationTime));
        }
        else if(interpolationTime < 1f)
        {
            interpolationTime += Time.deltaTime;
            abilityCollider.transform.localScale = new Vector3(Mathf.Lerp(maxScale, minScale, interpolationTime), Mathf.Lerp(maxScale, minScale, interpolationTime), Mathf.Lerp(maxScale, minScale, interpolationTime));
        }
    }

    #region ATTACKS & ABILITIES
    protected override void BasicAttack()
    {
        attackCollider.enabled = true;
        attackMeshRenderer.enabled = true;
        Invoke("DisableAttackCollider", 0.2f);
    }

    protected override void BasicAbility()
    {
        if (abilityTimer <= 0f && !isUltimateActive)
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
        if (ultimateTimer <= 0f)
        {
            isUltimateActive = true;
            movementSpeed *= ultimateSpeedMultiplier;
            playerController.SetSpeed(movementSpeed);
            playerController.ChangeState(PlayerController.State.INVENCIBILITY);
            abilityTimer = abilityCooldown; //cancelar BasicAbility
            ultimateTimer = ultimateCooldown + ultimateDuration;
        }
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
            if (abilityTimer < abilityCooldown && isAbiltyActive)
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
                isUltimateActive = false;
                movementSpeed = baseMovementSpeed;
                playerController.SetSpeed(movementSpeed);
            }
        }
    }
    #endregion

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
        attackMeshRenderer.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isUltimateActive)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(ultimateDamage);
        }
    }
}
