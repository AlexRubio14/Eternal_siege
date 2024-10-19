using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class RoguelikeCanvas : MonoBehaviour
{
    public static RoguelikeCanvas instance;

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
        instance = this;
        animator = GetComponentInChildren<Animator>();
    }

    public void LevelUp()
    {
        TimeManager.instance.PauseTime();
        PlayersManager.instance.ChangeActionMap("RoguelikeMenu");

        onFadeIn += SelectButton;


        switch (RoguelikeManager.instance.numOfPlayers)
        {
            case 1:
                animator.Play("FadeIn");
                break;
            case 2:
                animator.Play("FadeIn2Players");
                break;
            default:
                break;
        }

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
        TimeManager.instance.ResumeTime();
        switch (RoguelikeManager.instance.numOfPlayers)
        {
            case 1:
                animator.Play("FadeOut");
                break;
            case 2:
                animator.Play("FadeOut2Players");
                break;
            default:
                break;
        }
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
