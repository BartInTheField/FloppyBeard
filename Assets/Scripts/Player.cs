using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
  [SerializeField] private float jumpForce = 10f;
g  [SerializeField] private float deathForce = 4f;
  [SerializeField] private float rotationSpeed = 20f;
  [SerializeField] private AudioClip jumpAudio = null;
  [SerializeField] private AudioClip deathAudio = null;
  [SerializeField] private SpriteRenderer[] sprites = null;

  public event Action OnDeath;
  public event Action OnGoalTouched;

  private Vector2 deathImpactDirection;
  private float defaultGravityScale = 1f;
  private bool wantsToJump = false;
  private bool isDeath = false;
  private bool playedIsDeath = false;
  private InputManager inputManager;
  private Rigidbody2D rigidBody;
  private AudioSource audioSource;

  private void Awake()
  {
    // InputManager is always in the scene
    inputManager = FindObjectOfType<InputManager>();

    audioSource = GetComponent<AudioSource>();
    rigidBody = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    inputManager.OnJump += SetWantsToJump;

    // Do not start gravity till player is ready
    defaultGravityScale = rigidBody.gravityScale;
    DeactivateGravity();
    inputManager.OnPlayerReady += ActivateGravity;
  }

  private void Update()
  {
    if (rigidBody.velocity.y != 0)
    {
      if (rigidBody.velocity.y > 0)
      {
        // Upwards rotation
        RotateToDegrees(45, rotationSpeed * 1.5f);
      }
      else
      {
        // Downwards rotation
        RotateToDegrees(-45, rotationSpeed);
      }
    }
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
      // Fade out the player
      foreach (SpriteRenderer sprite in sprites)
      {
        sprite.DOFade(0, timeUntilFade);
      }

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
    audioSource.clip = jumpAudio;
    audioSource.Play();
    wantsToJump = true;
  }

  private void RotateToDegrees(float degrees, float speed, bool instant = false)
  {
    Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, degrees);
    if (!instant)
    {
      transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, (speed * 10) * Time.deltaTime);
    }
    else
    {
      transform.rotation = rotation;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Obstacle"))
    {
      if (!isDeath)
      {
        // Calculate point of impact
        deathImpactDirection = transform.position - other.transform.position;
        deathImpactDirection.Normalize();

        audioSource.clip = deathAudio;
        audioSource.Play();

        isDeath = true;
        OnDeath?.Invoke();
      }
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