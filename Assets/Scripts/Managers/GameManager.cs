using UnityEngine;

namespace CompleteProject
{
  using System;

  using Assets.Scripts.PowerUps;

  public class GameManager : MonoBehaviour
  {

    public PlayerStates playerStates;

    public static GameManager instance { get; private set; }
    void Awake()
    {
      instance = this;
    }

    public void ChangePlayerState(PlayerStates state)
    {
      this.playerStates = state;
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