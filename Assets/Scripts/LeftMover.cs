using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMover : MonoBehaviour
{
    // moveSpeed is gotton from GameManager because multiple GameObjects have this script
    // They all need to move on the exact same speed and GameManager is a good central location
    private float moveSpeed;
    private GameManager gameManager;

    private void Awake()
    {
        // GameManager is always in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        moveSpeed = gameManager.ScrollSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
