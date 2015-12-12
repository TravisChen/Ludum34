using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public tk2dSpriteAnimator spriteAnimator;

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
			spriteAnimator.Play( "RedBallIdle" );
		}
		else
		{
			spriteAnimator.Play( "BlueBallIdle" );
		}
	}

	void OnTriggerEnter(Collider other) {

		CollectController collect = other.gameObject.GetComponent<CollectController>();
		if( collect )
		{
			if( collect.CanCollect() )
			{
				Destroy( other.gameObject );
				gameController.Collect();
			}
		}
	}
}
