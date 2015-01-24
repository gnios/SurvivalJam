﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
  using Assets.Scripts.Managers;
  using Assets.Scripts.PowerUps;

  public class PlayerHealth : MonoBehaviour
  {
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public AudioClip cureClip;
    public AudioClip hurtClip;
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashDamageColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public Color flashLifeColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

    void Awake()
    {
      // Setting up the references.
      anim = GetComponent<Animator>();
      playerAudio = GetComponent<AudioSource>();
      playerMovement = GetComponent<PlayerMovement>();

      // Set the initial health of the player.
      currentHealth = startingHealth;
    }


    void Update()
    {
      // If the player has just been damaged...
      if (!damaged)
      {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
      }
      damaged = false;
    }


    public void TakeDamage(int amount)
    {
      if (PowerUpManager.instance.PowerUpActive != EnumPowerUps.Shield)
      {
        damageImage.color = flashLifeColour;
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.clip = hurtClip;
        playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
          Death();
        }
      }
    }

    public void RemoveDamage(int amount)
    {
      damageImage.color = flashDamageColour;
      currentHealth += amount;
      if (currentHealth > 100)
      {
        currentHealth = 100;
      }
      healthSlider.value = currentHealth;
      playerAudio.clip = cureClip;
      playerAudio.Play();
    }


    void Death()
    {
      // Set the death flag so this function won't be called again.
      isDead = true;

      // Tell the animator that the player is dead.
      anim.SetTrigger("Die");

      // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
      playerAudio.clip = deathClip;
      playerAudio.Play();

      // Turn off the movement and shooting scripts.
      playerMovement.enabled = false;
    }


    public void RestartLevel()
    {
      // Reload the level that is currently loaded.
      Application.LoadLevel(Application.loadedLevel);
    }
  }
}