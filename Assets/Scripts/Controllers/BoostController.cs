using UnityEngine;
using System.Collections;

public class BoostController : ObjectController {

	private tk2dSprite sprite; 
	private tk2dSpriteAnimator animator;
	private BoxCollider boxCollider;

	private bool boostSFXPlayed = false;

	private const float BOOST_FORCE = 1500.0f;

	// Use this for initialization
	override public void Start () {
	
		base.Start();

		sprite = GetComponent<tk2dSprite>();
		animator = GetComponent<tk2dSpriteAnimator>();
		boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {

		base.FixedUpdate();

		if( gameController.IsRed() && ObjectIsBlue() )
		{
			animator.Play( "BlueBoostOff" );
			boxCollider.enabled = false;
		}
		else if( gameController.IsBlue() && ObjectIsRed() )
		{
			animator.Play( "RedBoostOff" );
			boxCollider.enabled = false;
		}
		else
		{
			if( ObjectIsRed() )
			{
				animator.Play( "RedBoost" );
			}
			else
			{
				animator.Play( "BlueBoost" );
			}
			sprite.color = Color.white;
			boxCollider.enabled = true;
		}
	}

	void OnTriggerEnter(Collider other) {

		if( other.tag == "Ball" )
		{
			BallController ball = other.gameObject.GetComponent<BallController>();
			if( ball )
			{
				Rigidbody rigid = ball.GetComponent<Rigidbody>();
				rigid.AddForce(this.transform.up * BOOST_FORCE);

				if( !boostSFXPlayed )
				{
					RefManager.Instance.boostSFX.Play();
					boostSFXPlayed = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
		
		boostSFXPlayed = false;
	}
}
