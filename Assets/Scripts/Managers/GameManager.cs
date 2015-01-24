using UnityEngine;

namespace CompleteProject
{
  public class GameManager : MonoBehaviour
  {

    public PlayerStates playerStates;
    public float durationOfPowerUp = 3;
    public float currentTimePowerUp;
    private bool usingPowerUp;

    public static GameManager instance { get; private set; }
    void Awake()
    {
      instance = this;
      currentTimePowerUp = durationOfPowerUp;
    }

    void Update()
    {
      if (usingPowerUp)
      {
        currentTimePowerUp -= Time.deltaTime;
        if (currentTimePowerUp <= 0)
        {
          this.ChangePlayerState(PlayerStates.Normal);
        }
      }
    }

    public void ChangePlayerState(PlayerStates state)
    {
      this.playerStates = state;
      usingPowerUp = true;
      currentTimePowerUp = durationOfPowerUp;
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