using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject gameOverPanelObject = null;

    [SerializeField] private ImpulseFadeTextUI fadingButtonPrompt = null;

    private GameManager gameManager;
    private InputManager inputManager;

    private void Awake()
    {
        // GameManager is always in the scene
        gameManager = FindObjectOfType<GameManager>();
        // InputManager is always in the scene
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        gameManager.OnScoreChanged += SetScore;
        gameManager.OnGameOver += ShowGameOver;
        inputManager.OnPlayerReady += StopButtonPrompt;
    }

    private void SetScore(int score)
    {
        scoreText.text = score + "";
    }

    private void ShowGameOver(int finalScore)
    {
        gameOverPanelObject.SetActive(true);
        GameOverPanel gameOverPanel = gameOverPanelObject.GetComponent<GameOverPanel>();
        gameOverPanel.SetScore(finalScore);
    }

    private void StopButtonPrompt()
    {
        fadingButtonPrompt.StopFading();
    }
}
