using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reviveController : MonoBehaviour
{

    private PlayerController playerController;

    [SerializeField] private float timeToRevive;
    private float currentTimeToRevive;

    [SerializeField] private SpriteRenderer sprite;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();

        currentTimeToRevive = 0.0f;
    }

    private void PlayerReviving()
    {
        currentTimeToRevive += Time.deltaTime;
        Debug.Log(currentTimeToRevive);

        sprite.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentTimeToRevive / timeToRevive);

        if (currentTimeToRevive >= timeToRevive)
        {
            playerController.Revive();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerReviving();
        }
    }

    private void OnDisable()
    {
        currentTimeToRevive = 0.0f;
    }
}
