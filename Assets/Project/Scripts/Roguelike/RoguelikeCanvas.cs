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

    public void FadeIn()
    {
        animator.Play("FadeIn");
    }

    public void FadeOut()
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
