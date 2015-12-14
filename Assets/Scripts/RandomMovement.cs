using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {

	public float speed = 10.0f;
	public tk2dSprite sprite;

	private Vector3 currWaypoint;
	private Vector3 direction;

	private Rigidbody rigidBody;

	private float changeTime;

	private const float MIN_CHANGE_TIME = 0.5f;
	private const float MAX_CHANGE_TIME = 2.0f;
	private const float RANDOM_RADIUS = 2.0f;

	// Use this for initialization
	void Start () {
	
		rigidBody = GetComponent<Rigidbody>();
		changeTime = Random.Range( MIN_CHANGE_TIME, MAX_CHANGE_TIME );
		ChangeDirection();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		rigidBody.AddForce( direction.normalized * speed );

		changeTime -= Time.deltaTime;
		if( changeTime <= 0.0f )
		{
			ChangeDirection();
			changeTime = Random.Range( MIN_CHANGE_TIME, MAX_CHANGE_TIME );
		}

		if( rigidBody.velocity.x <= 0.0f )
		{
			sprite.FlipX = true;
		}
		else
		{
			sprite.FlipX = false;
		}

	}

	private void ChangeDirection()
	{
		currWaypoint.x = transform.position.x + Random.Range( -RANDOM_RADIUS, RANDOM_RADIUS );
		currWaypoint.y = transform.position.y + Random.Range( -RANDOM_RADIUS, RANDOM_RADIUS );
		currWaypoint.z = 0.0f;

		direction = transform.position - currWaypoint;

	}
}
