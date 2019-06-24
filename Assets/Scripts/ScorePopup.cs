using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(Rigidbody2D), typeof(TextMeshPro))]
public class ScorePopup : MonoBehaviour
{
    [SerializeField] private float upForce = 10f;
    [SerializeField] private float fadeSpeed = 2f;

    private Rigidbody2D rigidBody;
    private CanvasGroup canvasGroup;
    private TextMeshPro textMesh;

    private bool isScoreSet = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        textMesh = GetComponent<TextMeshPro>();
        textMesh.alpha = 0;
    }

    private void FixedUpdate()
    {
        if (isScoreSet)
        {
            rigidBody.AddForce(Vector2.up * upForce * Time.deltaTime);
        }
    }

    public void SetScore(int score)
    {
        textMesh.text = score + "";
        textMesh.alpha = 1;
        isScoreSet = true;
        textMesh.DOFade(0, fadeSpeed);
        Destroy(gameObject, fadeSpeed);
    }
}
