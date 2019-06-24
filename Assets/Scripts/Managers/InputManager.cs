using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private string JOYSTICK_A = "joystick button 0";

    public event Action OnJump;
    public event Action OnPlayerReady;

    private bool playerReady = false;
    private bool isRestarting = false;
    private Fader fader;
    private GameManager gameManager;

    private void Awake()
    {
        // Fader is always in the scene
        fader = FindObjectOfType<Fader>();
        // GameManager is always in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!gameManager.IsGameOver)
        {
            // Game is not yet over 

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(JOYSTICK_A))
            {
                if (!playerReady)
                {
                    OnPlayerReady?.Invoke();
                    playerReady = true;
                }

                OnJump?.Invoke();
            }
        }
        else
        {
            // Game is over
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(JOYSTICK_A))
            {
                if (!isRestarting)
                {
                    RestartGameClick();
                }
            }
        }

    }

    public void RestartGameClick()
    {
        isRestarting = true;
        fader.FadeIn();
        StartCoroutine(AwaitRestart(fader.FadeSpeed));
    }

    private IEnumerator AwaitRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
