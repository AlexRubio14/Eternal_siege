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

    [SerializeField] 


    private void Awake()
    {
        instance = this;
        animator = GetComponentInChildren<Animator>();
    }

    public void LevelUp()
    {
        TimeManager.instance.PauseTime();
        onFadeIn += ChangeActionMapToRoguelikeMenu;
        onFadeIn += SelectButton;


         switch (RoguelikeManager.instance.numOfPlayers)
         {
            case 1:
                animator.Play("FadeIn");
                onFadeIn += RoguelikeManager.instance.Active1PlayerUpgradeCanvas;
                break;
            case 2:
                animator.Play("FadeIn2Players");
                onFadeIn += RoguelikeManager.instance.Active2PlayersUpgradeCanvas;
                break;
            default:
                break;
         }
    }

    private void ChangeActionMapToRoguelikeMenu()
    {
        PlayersManager.instance.ChangeActionMap("RoguelikeMenu");
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
