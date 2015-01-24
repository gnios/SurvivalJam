using UnityEngine;
using System.Collections;

namespace CompleteProject
{
  using System.Linq;

  public class EnemyMovement : MonoBehaviour
  {
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    public EnemyStates enemyStates;

    void Awake()
    {
      // Set up the references.
      player = GameObject.FindGameObjectWithTag("Player").transform;
      playerHealth = player.GetComponent<PlayerHealth>();
      enemyHealth = GetComponent<EnemyHealth>();
      nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
      // If the enemy and the player have health left...
      if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
      {
        this.player = GameManager.instance.SeekPlayerNext(this.transform.position).transform;
        nav.SetDestination(player.position);
      }
        // Otherwise...
      else
      {
        // ... disable the nav mesh agent.
        nav.enabled = false;
      }
    }
  }
}