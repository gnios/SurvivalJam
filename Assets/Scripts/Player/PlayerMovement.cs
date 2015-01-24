using CompleteProject;

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
  private Transform myTransform;				// this transform
  private Vector3 destinationPosition;		// The destination Point
  private float destinationDistance;			// The distance between myTransform and destinationPosition
  private float currentMoveSpeed;						// The Speed the character will move
  public int dashDamage;
  public float moveSpeed = 50;
  public float dashMaxTime = 1f;
  public float dashCurrentTime = 0f;

  void Start()
  {
    myTransform = transform;							// sets myTransform to this GameObject.transform
    destinationPosition = myTransform.position;			// prevents myTransform reset
    dashCurrentTime = dashMaxTime;
  }

  void Update()
  {

    // keep track of the distance between this gameObject and destinationPosition
    destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);

    if (destinationDistance < .5f || dashCurrentTime <= 0)
    {		// To prevent shakin behavior when near destination
      this.currentMoveSpeed = 0;
      GameManager.instance.ChangePlayerState(PlayerStates.Stopped);
      rigidbody.velocity = Vector3.zero;
    }
    else if (destinationDistance > .5f || dashCurrentTime > 0)
    {			// To Reset Speed to default
      this.currentMoveSpeed = moveSpeed;
      GameManager.instance.ChangePlayerState(PlayerStates.Dash);
    }


    // Moves the Player if the Left Mouse Button was clicked
    if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
    {

      Plane playerPlane = new Plane(Vector3.up, myTransform.position);
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      float hitdist = 0.0f;

      if (playerPlane.Raycast(ray, out hitdist) &&  GameManager.instance.playerStates != PlayerStates.Dash)
      {
        destinationPosition = ray.GetPoint(hitdist);
        myTransform.rotation = Turning(ray);
        dashCurrentTime = dashMaxTime;
      }
    }

    this.TimerDash();
    // To prevent code from running if not needed
    if (destinationDistance > .5f)
    {
      Vector3 direction = (destinationPosition - myTransform.position).normalized;
      rigidbody.AddForce((destinationPosition - transform.position).normalized * (currentMoveSpeed * 10000) * Time.smoothDeltaTime);
    }
  }
  private Quaternion Turning(Ray ray)
  {
    return Quaternion.LookRotation(destinationPosition - transform.position);
  }

  private void TimerDash()
  {
    dashCurrentTime -= Time.deltaTime;
    if (dashCurrentTime <= 0)
    {
      dashCurrentTime = 0;
    }
  }

  void OnCollisionEnter(Collision collision)
  {
    {

      // Try and find an EnemyHealth script on the gameobject hit.
      EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();

      // If the EnemyHealth component exist...
      if (enemyHealth != null)
      {
        if (GameManager.instance.playerStates == PlayerStates.Dash)
        {
          // ... the enemy should take damage.
          enemyHealth.TakeDamage(dashDamage, gameObject.transform.position);
        }
      }
    }
  }
}