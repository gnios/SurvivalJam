using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Managers
{
  using Assets.Scripts.PowerUps;

  using CompleteProject;

  using UnityEngine;

  public class PowerUpManager : MonoBehaviour
  {
    public static PowerUpManager instance { get; private set; }
    void Awake()
    {
      instance = this;
    }

    public float currentTimePowerUp;
    private bool usingPowerUp;
    public EnumPowerUps PowerUpActive;
    public float durationOfPowerUp = 30;
    private Shield[] shields;
    private bool shieldActive = false;

    void Start()
    {
      currentTimePowerUp = durationOfPowerUp;
      usingPowerUp = false;
      shields = FindObjectsOfType<Shield>();
    }

    void Update()
    {
      if (usingPowerUp)
      {
        currentTimePowerUp -= Time.deltaTime;
        if (currentTimePowerUp <= 0)
        {
          this.ApplyPowerUp(EnumPowerUps.NoPowerUps);
        }
      }
      if (shieldActive)
      {
        PowerUpActive = EnumPowerUps.Shield;
      }
    }
    public void ApplyPowerUp(EnumPowerUps powerUp)
    {
      usingPowerUp = true;
      switch (powerUp)
      {
        case EnumPowerUps.NoPowerUps:
          RemoveAllPowerUps();
          break;
        case EnumPowerUps.Life:
          ApplyLifePowerUp();
          break;
        case EnumPowerUps.Shield:
          ShieldPowerUpActive(true);
          break;
      }
      if (!shieldActive)
      {
        PowerUpActive = powerUp;
      }
    }

    private void RemoveAllPowerUps()
    {
      PowerUpActive = EnumPowerUps.NoPowerUps;
      usingPowerUp = false;
      currentTimePowerUp = durationOfPowerUp;
      ShieldPowerUpActive(false);
      shieldActive = false;
    }

    private static void ShieldPowerUpActive(bool option)
    {
      if (option)
      {
        instance.shieldActive = true;
      }
      foreach (var shield in instance.shields)
      {
        shield.collider.enabled = option;
        shield.renderer.enabled = option;
      }
    }
    private static void ApplyLifePowerUp()
    {
      PlayerHealth[] players = FindObjectsOfType<PlayerHealth>() as PlayerHealth[];
      {
        foreach (var playerHealth in players)
        {
          playerHealth.RemoveDamage(30);
        }
      }
    }
  }
}
