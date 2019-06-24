using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private GameObject scorePopUp = null;
    [SerializeField] private BoxCollider2D groundCollider = null;
    [SerializeField, Range(0f, 1f)]
    private float popupXOffset = 0.5f;
    [SerializeField, Range(0f, 1f)]
    private float popupYOffset = 0.5f;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnGameOver;

    private int score = 0;
    private bool isGameOver = false;
    private Player player;
    private Fader fader;

    public bool IsGameOver { get => isGameOver; }
    public float ScrollSpeed { get => scrollSpeed; }

    private void Awake()
    {
        // Player is always in the scene
        player = FindObjectOfType<Player>();
        //Fader is always in the scene
        fader = FindObjectOfType<Fader>();
    }

    private void Start()
    {
        fader.FadeOut();
        player.OnDeath += GameOver;
        player.OnGoalTouched += PlayerHitGoal;
    }

    private void GameOver()
    {
        groundCollider.enabled = false;
        isGameOver = true;
        OnGameOver?.Invoke(score);
    }

    private void PlayerHitGoal()
    {
        score += 1;
        OnScoreChanged?.Invoke(score);

        // Spawn the PopUp
        GameObject newPlusOnePopup = Instantiate(scorePopUp,
        new Vector2(player.transform.position.x + popupXOffset, player.transform.position.y + popupYOffset),
        Quaternion.identity);

        newPlusOnePopup.GetComponent<ScorePopup>().SetScore(score);

    }
}
