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

		if( gameController.IsGameOver() )
		{
			body.isKinematic = true;
			body.detectCollisions = false;
		}

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
				if( collect.ObjectIsRed() )
				{
					GameObject.Instantiate( RefManager.Instance.redCollectParticle, new Vector3( other.transform.position.x, other.transform.position.y, 0.0f ), other.transform.rotation );
				}
				else
				{
					GameObject.Instantiate( RefManager.Instance.blueCollectParticle, new Vector3( other.transform.position.x, other.transform.position.y, 0.0f ), other.transform.rotation );
				}

				RefManager.Instance.collectSFX.Play();
				Destroy( other.gameObject );
				gameController.Collect();
			}
		}

		PortalEntranceController portalEntrance = other.gameObject.GetComponent<PortalEntranceController>();
		if( portalEntrance )
		{
			GameObject closestExit = FindClosestPortalExit( portalEntrance );
			if( closestExit )
			{
				RefManager.Instance.portalSFX.Play();
				transform.position = closestExit.transform.position;
			}
		}
	}

	GameObject FindClosestPortalExit( PortalEntranceController portalEntranceController ) {
		
		GameObject[] portalExits;
		portalExits = GameObject.FindGameObjectsWithTag("PortalExit");
		GameObject closestPortalExit = null;

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject portalExit in portalExits) {

			PortalExitController portalExitController = portalExit.GetComponent<PortalExitController>();
			if( ( portalExitController.ObjectIsRed() && portalEntranceController.ObjectIsRed() ) || 
				( portalExitController.ObjectIsBlue() && portalEntranceController.ObjectIsBlue() ) )
			{
				if( portalExitController.transform.position.y > portalEntranceController.transform.position.y )
				{
					Vector3 diff = portalExit.transform.position - position;
					float curDistance = diff.sqrMagnitude;
					if (curDistance < distance) {
						closestPortalExit = portalExit;
						distance = curDistance;
					}
				}
			}
		}
		return closestPortalExit;
	}

	public void ballPulse( bool right ) 
	{
		if( right )
		{
			body.AddForce(Vector3.right * 100.0f);
		}
		else
		{
			body.AddForce(Vector3.left * 100.0f);
		}
	}

	void OnCollisionEnter(Collision collision) {

		if( collision.gameObject.layer ==  LayerMask.NameToLayer("Walls") )
		{
			RefManager.Instance.wallHitSFX.Play();
			gameController.WallHit();
		}

		EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
		if( enemy )
		{
			GameObject.Instantiate( RefManager.Instance.enemyHitParticle, new Vector3( collision.transform.position.x, collision.transform.position.y, 0.0f ), collision.transform.rotation );

			enemy.EnemyHit();
			gameController.EnemyHit();

			Destroy( enemy.gameObject );
		}
	}
}
