using Assets.Scripts.Managers;
using Assets.Scripts.PowerUps;

using CompleteProject;

using UnityEngine;
using System.Collections;

public class LifePowerUp : PowerUp
{
  public override void BehaviuourTrigger()
  {
    PowerUpManager.instance.ApplyPowerUp(EnumPowerUps.Life);
    Debug.Log("LifePowerUp");
  }
}
