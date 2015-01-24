using UnityEngine;
using System.Collections;

namespace CompleteProject
{
  public class EnemyAttack : MonoBehaviour
  {
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.

    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.
    private EnemyStates enemyStates;

    void Awake()
    {
      // Setting up the references.
      InitializePlayer();
      enemyHealth = GetComponent<EnemyHealth>();
      anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
      // If the entering collider is the player...
      if (other.gameObject == player)
      {
        // ... the player is in range.
        playerInRange = true;
      }
    }


    void OnTriggerExit(Collider other)
    {
      // If the exiting collider is the player...
      if (other.gameObject == player)
      {
        // ... the player is no longer in range.
        playerInRange = false;
      }
    }


    void Update()
    {
      InitializePlayer();
      // Add the time since Update was last called to the timer.
      timer += Time.deltaTime;

      if (timer >= timeBetweenAttacks
          && enemyHealth.currentHealth > 0)
      {
        enemyStates = EnemyStates.Attacking;
      }
      else
      {
        enemyStates = EnemyStates.Walking;
      }

      // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
      if (enemyStates == EnemyStates.Attacking && playerInRange && enemyHealth.currentHealth > 0)
      {
        // ... attack.
        Attack();
      }

      

      // If the player has zero or less health...
      if (playerHealth.currentHealth <= 0)
      {
        // ... tell the animator the player is dead.
        anim.SetTrigger("PlayerDead");
        Debug.Log("Game OVER");
      }
    }

    protected virtual void Attack()
    {
      // Reset the timer.
      timer = 0f;

      // If the player has health to lose...
      if (playerHealth.currentHealth > 0)
      {
        if (GameManager.instance.playerStates != PlayerStates.Dash)
        {
          // ... damage the player.
          playerHealth.TakeDamage(attackDamage);
        }
      }
    }

    void InitializePlayer()
    {
      this.player = GameManager.instance.SeekPlayerNext(transform.position);
      this.playerHealth = player.GetComponent<PlayerHealth>();
    }
  }
}