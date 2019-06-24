using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;

    private int score = 0;
    private Player player;

    public float ScrollSpeed
    {
        get
        {
            return scrollSpeed;
        }
    }

    private void Awake()
    {
        // Player is always in the scene
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        player.OnDead += GameOver;
        player.OnGoalTouched += AddOneToScore;
    }

    private void GameOver()
    {
        Debug.Log("Game over");
    }

    private void AddOneToScore()
    {
        score += 1;
        Debug.Log(score);
    }
}
