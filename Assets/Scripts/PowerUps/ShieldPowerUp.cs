﻿using Assets.Scripts.Managers;
using Assets.Scripts.PowerUps;

using CompleteProject;

using UnityEngine;
using System.Collections;

public class ShieldPowerUp : PowerUp
{
  public override void BehaviuourTrigger()
  {
    PowerUpManager.instance.ApplyPowerUp(EnumPowerUps.Shield);
    Debug.Log("ShieldPowerUp");
  }
}
