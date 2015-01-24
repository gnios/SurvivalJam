using Assets.Scripts.Managers;

using CompleteProject;

using UnityEngine;
using System.Collections;

public class ShieldPowerUp : PowerUp
{
  public override void BehaviuourTrigger()
  {
    GameManager.instance.ChangePlayerState(PlayerStates.Shield);
    Debug.Log("ShieldPowerUp");
  }
}
