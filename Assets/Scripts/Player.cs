using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField]
    private Color deathColor = new Color(0, 0, 0);
    [SerializeField] private float timeUntilFade = 2f;
    [SerializeField] private float deathForce = 4f;

    public event Action OnDeath;
    public event Action OnGoalTouched;

    private Vector2 deathImpactDirection;
    private float defaultGravityScale = 1f;
    private bool wantsToJump = false;
    private bool isDeath = false;
    private bool playedIsDeath = false;
    private InputManager inputManager;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // InputManager is always in the scene
        inputManager = FindObjectOfType<InputManager>();

        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        inputManager.OnJump += SetWantsToJump;

        // Do not start gravity till player is ready
        defaultGravityScale = rigidBody.gravityScale;
        DeactivateGravity();
        inputManager.OnPlayerReady += ActivateGravity;
    }

    private void FixedUpdate()
    {
        if (wantsToJump && !isDeath)
        {
            // Zero out the current velocity 
            rigidBody.velocity = Vector2.zero;
            // Give the player upward force
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Set to false to make sure next frame is no longer jump frame
            wantsToJump = false;
        }

        if (isDeath && !playedIsDeath)
        {
            // Zero out the current velocity 
            rigidBody.velocity = Vector2.zero;
            // Make player death color
            spriteRenderer.color = deathColor;
            // Fade out the player
            spriteRenderer.DOFade(0, timeUntilFade);

            // Push player in opposite direction of impact
            DisableFreeze();
            rigidBody.gravityScale = 0;
            rigidBody.AddForce(deathImpactDirection * deathForce, ForceMode2D.Impulse);

            playedIsDeath = true;

            //Destory player
            Destroy(gameObject, timeUntilFade);
        }

    }

    private void DisableFreeze()
    {
        rigidBody.constraints = RigidbodyConstraints2D.None;
    }

    private void SetWantsToJump()
    {
        wantsToJump = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // Calculate point of impact
            deathImpactDirection = transform.position - other.transform.position;
            deathImpactDirection.Normalize();

            isDeath = true;
            OnDeath?.Invoke();
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            if (!isDeath)
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
        rigidBody.gravityScale = defaultGravityScale;
    }

}
