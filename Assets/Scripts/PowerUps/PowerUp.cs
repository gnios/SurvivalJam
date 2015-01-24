using Assets.Scripts.Managers;

using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour
{

  private float valueFadeTo = -1;
  public float lifeTime = 20f;
  // Use this for initialization
  void Start()
  {
    InvokeRepeating("Fade", 2, 0.5F);
  }

  // Update is called once per frame
  void Update()
  {
    lifeTime -= Time.deltaTime;


    if (lifeTime <= 0)
    {
      Destroy(gameObject);
    }
  }

  void Fade()
  {
    iTween.FadeTo(gameObject, valueFadeTo, 3);
    valueFadeTo = valueFadeTo * -1;
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == Tags.PLAYER)
    {
      Destroy(gameObject);
      BehaviuourTrigger();
    }
  }
  public abstract void BehaviuourTrigger();

}
