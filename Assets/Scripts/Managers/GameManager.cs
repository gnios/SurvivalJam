using UnityEngine;

namespace CompleteProject
{
  public class GameManager : MonoBehaviour
  {

    public PlayerStates playerStates;       // Reference to the player's heatlh.
    public EnemyStates enemyStates;                // The enemy prefab to be spawned.
    public static GameManager instance { get; private set; }

    //When the object awakens, we assign the static variable
    void Awake()
    {
      instance = this;
    }

    public void ChangePlayerState(PlayerStates state)
    {
      this.playerStates = state;
    }

    public void ChangeEnemyState(EnemyStates state)
    {
      this.enemyStates = state;
    }

    public GameObject SeekPlayerNext(Vector3 point)
    {
      var players = GameObject.FindGameObjectsWithTag("Player");
      GameObject playerNext = null;
      float shortestDistance = 0;
      float distanceAux = 10000f;
      foreach (var playerGame in players)
      {
        shortestDistance = Vector3.Distance(point, playerGame.transform.position);

        if (shortestDistance < distanceAux)
        {
          playerNext = playerGame;
          distanceAux = shortestDistance;
        }
      }
      return playerNext;
    }
  }
}