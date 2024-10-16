using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Security.Cryptography;
using Unity.Mathematics;

public class RoguelikeCanvas : MonoBehaviour
{
    public static RoguelikeCanvas Instance;

    [SerializeField] private Image panel;

    public Action onFadeIn;

    public Action onFadeOut;

    private Animator animator;

    [SerializeField] public Button onePlayerButton;
    [SerializeField] public Button twoPlayersButtonOne;
    [SerializeField] public Button twoPlayersButtonTwo;

    [SerializeField] private float timeToEndPickUpgrade;


    private void Awake()
    {
        Instance = this;
        animator = GetComponentInChildren<Animator>();
    }

    public void LevelUp()
    {
        TimeManager.instance.PauseTime();
        PlayersManager.instance.ChangeActionMap("RoguelikeMenu");

        onFadeIn += SelectButton;
        animator.Play("FadeIn");
    }

    IEnumerator EndPickUpgrade()
    {
        yield return timeToEndPickUpgrade;

        float randomValue = UnityEngine.Random.Range(0, 3);


    }

    private void SelectButton()
    {
        if (Input.GetJoystickNames().Length == 1)
        {
            onePlayerButton.Select();
        }
    }

    public void ReturnToGameplay()
    {
        animator.Play("FadeOut");
    }

    public void OnFadeIn()
    {
        if (onFadeIn != null) onFadeIn();
    }

    public void OnFadeOut()
    {
        if (onFadeOut != null) onFadeOut();
    }
}
