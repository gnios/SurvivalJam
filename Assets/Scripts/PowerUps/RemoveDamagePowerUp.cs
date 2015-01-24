using Assets.Scripts.Managers;

using CompleteProject;

using UnityEngine;
using System.Collections;

public class RemoveDamagePowerUp : PowerUp
{
  public override void BehaviuourTrigger()
  {
    PlayerHealth[] players = FindObjectsOfType<PlayerHealth>() as PlayerHealth[];
    {
      foreach (var playerHealth in players)
      {
        playerHealth.RemoveDamage(30);
      }
    }
    Debug.Log("RemoveDamagePowerUp");
  }
}
