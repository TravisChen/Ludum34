using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public tk2dSpriteAnimator spriteAnimator;
	private bool isRed = true;

	public PlayerCamera playerCamera;
	public GameController gameController;

	// Use this for initialization
	void Start () {
	
		playerCamera = GameObject.FindGameObjectWithTag( "PlayerCamera" ).GetComponent<PlayerCamera>();
		playerCamera.SetTarget( this.transform );

		gameController = GameObject.FindGameObjectWithTag( "GameController" ).GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Input.GetKeyDown(KeyCode.LeftShift) )
		{
			isRed = true;
		}

		if( Input.GetKeyDown(KeyCode.RightShift) )
		{
			isRed = false;
		}

		if( isRed )
		{
			spriteAnimator.Play( "RedBallIdle" );
		}
		else
		{
			spriteAnimator.Play( "BlueBallIdle" );
		}
	}

	void OnTriggerEnter(Collider other) {
		Destroy( other.gameObject );
		gameController.Collect();
	}
}
