using UnityEngine;
using System.Collections;

public class BoostController : ObjectController {

	private tk2dSprite sprite; 
	private BoxCollider boxCollider;

	private const float BOOST_FORCE = 2000.0f;

	// Use this for initialization
	override public void Start () {
	
		base.Start();

		sprite = GetComponent<tk2dSprite>();
		boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {

		base.FixedUpdate();

		if( gameController.IsRed() && ObjectIsBlue() )
		{
			sprite.color = UIManager.Instance.transparentColor;
			boxCollider.enabled = false;
		}
		else if( gameController.IsBlue() && ObjectIsRed() )
		{
			sprite.color = UIManager.Instance.transparentColor;
			boxCollider.enabled = false;
		}
		else
		{
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
			}
		}
	}
}
