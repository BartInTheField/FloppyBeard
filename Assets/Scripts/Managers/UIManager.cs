using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject gameOverPanelObject = null;

    private GameManager gameManager;

    private void Awake()
    {
        // GameManager is always in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        gameManager.OnScoreChanged += SetScore;
        gameManager.OnGameOver += ShowGameOver;
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
}
