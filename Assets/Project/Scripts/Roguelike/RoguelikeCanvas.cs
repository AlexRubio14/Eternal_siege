using UnityEngine;
using System;
using UnityEngine.UI;

public class RoguelikeCanvas : MonoBehaviour
{
    public static RoguelikeCanvas Instance;

    [SerializeField] private Image panel;

    public Action onFadeIn;

    public Action onFadeOut;

    private Animator animator;

    private void Awake()
    {
        Instance = this;
        animator = GetComponentInChildren<Animator>();
    }

    public void LevelUp()
    {
        InputController.Instance.ChangeActionMap("RoguelikeMenu");
        animator.Play("FadeIn");


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
