using UnityEngine;

public class Tank : Character
{
    [SerializeField] private GameObject attackCollider;

    [SerializeField] private float abilityDuration;
    [SerializeField] private GameObject abilityCollider;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float abilitySpeedMultiplier;
    [SerializeField] private float abulityCameraSpeed;
    private float interpolationTime;
    private bool isAbiltyActive;

    [SerializeField] private float ultimateDuration;
    [SerializeField] private float ultimateSpeedMultiplier;
    [SerializeField] private float ultimateDamage;
    private bool isUltimateActive;

    private Animator anim;

    [SerializeField] private float shakeDuration;
    [SerializeField] protected float shakeStrenght;

    private void Start()
    {
        base.Start();
        DisableAttackCollider();
        interpolationTime = 1f;
        isAbiltyActive = false;
        isUltimateActive = false;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        base.Update();
        if (isAbiltyActive && interpolationTime < 1f)
        {
            interpolationTime += Time.deltaTime;
            abilityCollider.transform.localScale = new Vector3(Mathf.Lerp(minScale, maxScale, interpolationTime), Mathf.Lerp(minScale, maxScale, interpolationTime), Mathf.Lerp(minScale, maxScale, interpolationTime));
            Camera.main.GetComponent<CameraController>().IncreaseDecreaseCamareInTankAbility(false, 0.5f, abulityCameraSpeed);
        }
        else if(interpolationTime < 1f)
        {
            interpolationTime += Time.deltaTime;
            abilityCollider.transform.localScale = new Vector3(Mathf.Lerp(maxScale, minScale, interpolationTime), Mathf.Lerp(maxScale, minScale, interpolationTime), Mathf.Lerp(maxScale, minScale, interpolationTime));
            Camera.main.GetComponent<CameraController>().IncreaseDecreaseCamareInTankAbility(true, 0.5f, abulityCameraSpeed);
        }
    }

    #region ATTACKS & ABILITIES
    protected override void BasicAttack()
    {
        attackCollider.SetActive(true);
        Invoke("DisableAttackCollider", 0.15f);
        AudioManager.instance.Play2dOneShotSound(basicAttackClip, "Sfx", 1, 0.5f, 0.75f);

    }

    protected override void BasicAbility()
    {
        if (abilityTimer <= 0f && !isUltimateActive)
        {
            isAbiltyActive = true;
            playerController.SetIsShielding(true);
            interpolationTime = 0f;
            movementSpeed *= abilitySpeedMultiplier;
            playerController.SetSpeed(movementSpeed);
            abilityTimer = abilityCooldown + abilityDuration;
            AudioManager.instance.Play2dOneShotSound(basicAbilityClip, "Sfx", 1, 0.5f, 0.75f);
        }
    }

    protected override void UltimateAbility()
    {
        if (ultimateTimer <= 0f)
        {
            CameraShaker.instance.Shake(shakeDuration, shakeStrenght);
            isUltimateActive = true;
            movementSpeed *= ultimateSpeedMultiplier;
            playerController.SetSpeed(movementSpeed);
            playerController.ChangeState(PlayerController.State.INVENCIBILITY);
            if(isAbiltyActive)
             abilityTimer = abilityCooldown; //cancelar BasicAbility
            ultimateTimer = ultimateCooldown + ultimateDuration;
            anim.SetBool("Walking", true);
            AudioManager.instance.Play2dOneShotSound(ultimateAbilityClip, "Sfx");
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
                playerController.SetIsShielding(false);
                movementSpeed = baseMovementSpeed;
                playerController.SetSpeed(movementSpeed);
                interpolationTime = 0f;
                UIManager.instance.InitTimer(false, this);
            }
        }
    }

    protected override void UpdateUltimateTimer()
    {
        if (ultimateTimer > 0f)
        {
            ultimateTimer -= Time.deltaTime;
            if (ultimateTimer < ultimateCooldown && isUltimateActive)
            {
                isUltimateActive = false;
                playerController.ChangeState(PlayerController.State.IDLE);
                movementSpeed = baseMovementSpeed;
                playerController.SetSpeed(movementSpeed);
                UIManager.instance.InitTimer(true, this);
            }
        }
    }
    #endregion

    private void DisableAttackCollider()
    {
        attackCollider.SetActive(false);
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
