using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private float deadForce = 20f;

    public event Action OnDead;
    public event Action OnGoalTouched;

    private bool wantsToJump = false;
    private bool isDead = false;
    private InputManager inputManager;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        // InputManager is always in the scene
        inputManager = FindObjectOfType<InputManager>();

        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        inputManager.OnJump += SetWantsToJump;

        // Do not start gravity till player is ready
        DeactivateGravity();
        inputManager.OnPlayerReady += ActivateGravity;
    }

    private void FixedUpdate()
    {
        if (wantsToJump && !isDead)
        {
            // Zero out the current velocity 
            rigidBody.velocity = Vector2.zero;
            // Give the player upward force
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Set to false to make sure next frame is no longer jump frame
            wantsToJump = false;
        }

        if (isDead)
        {
            // Zero out the current velocity 
            rigidBody.velocity = Vector2.zero;

            // Give the player downward force
            rigidBody.AddForce(Vector2.down * deadForce, ForceMode2D.Impulse);
        }
    }

    private void SetWantsToJump()
    {
        wantsToJump = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isDead = true;
            OnDead?.Invoke();
        }

        if (other.CompareTag("Goal"))
        {
            if (!isDead)
            {
                OnGoalTouched?.Invoke();
            }
        }
    }

    private void DeactivateGravity()
    {
        rigidBody.gravityScale = 0;
    }

    private void ActivateGravity()
    {
        rigidBody.gravityScale = 1;
    }

}
