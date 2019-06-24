using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ImpulseFadeTextUI : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 0.5f;

    private bool doFade = true;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.alpha = 0;
    }

    private void Start()
    {
        StartCoroutine(StartFading());
    }

    private IEnumerator StartFading()
    {
        textMesh.DOFade(1, fadeSpeed);

        yield return new WaitForSeconds(fadeSpeed);

        textMesh.DOFade(0, fadeSpeed);

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
