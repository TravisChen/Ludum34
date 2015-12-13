using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public tk2dSpriteAnimator spriteAnimator;
	public TrailRenderer trail;

	private GameController gameController;

	private Rigidbody body;

	// Use this for initialization
	void Start () {

		body = GetComponent<Rigidbody>();
		body.isKinematic = true;
		body.detectCollisions = false;

		gameController = GameObject.FindGameObjectWithTag( "GameController" ).GetComponent<GameController>();
	}

	public void BallActive()
	{
		body.isKinematic = false;
		body.detectCollisions = true;
	}
	
	// Update is called once per frame
	void Update () {

		if( gameController.IsRed() )
		{
			trail.material = RefManager.Instance.redTrailMat;
			spriteAnimator.Play( "RedBallIdle" );
		}
		else
		{
			trail.material = RefManager.Instance.blueTrailMat;
			spriteAnimator.Play( "BlueBallIdle" );
		}
	}

	void OnTriggerEnter(Collider other) {

		CollectController collect = other.gameObject.GetComponent<CollectController>();
		if( collect )
		{
			if( collect.CanCollect() )
			{
				if( collect.ObjectIsRed() )
				{
					GameObject.Instantiate( RefManager.Instance.redCollectParticle, new Vector3( other.transform.position.x, other.transform.position.y, 0.0f ), other.transform.rotation );
				}
				else
				{
					GameObject.Instantiate( RefManager.Instance.blueCollectParticle, new Vector3( other.transform.position.x, other.transform.position.y, 0.0f ), other.transform.rotation );
				}

				Destroy( other.gameObject );
				gameController.Collect();
			}
		}
	}

	void OnCollisionEnter(Collision collision) {

		if( collision.gameObject.layer ==  LayerMask.NameToLayer("Walls") )
		{
			gameController.WallHit();
		}
	}
}
