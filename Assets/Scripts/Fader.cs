using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1f;

    public float FadeSpeed { get => fadeSpeed; }

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, fadeSpeed);
    }

    public void FadeOut()
    {
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, fadeSpeed);
    }
}
