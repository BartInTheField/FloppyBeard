using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class ImpulseFadeCanvasGroup : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 0.5f;

    private bool doFade = true;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        StartCoroutine(StartFading());
    }

    private IEnumerator StartFading()
    {
        canvasGroup.DOFade(1, fadeSpeed);

        yield return new WaitForSeconds(fadeSpeed);

        canvasGroup.DOFade(0, fadeSpeed);

        yield return new WaitForSeconds(fadeSpeed);

        if (doFade)
        {
            StartCoroutine(StartFading());
        }
    }

    public void StopFading()
    {
        doFade = false;
    }
}
