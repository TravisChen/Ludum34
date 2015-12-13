using UnityEngine;
using System.Collections;

public class FlipperController : ObjectController {

	public Rigidbody flipper;
	public float flipperForce;
	public tk2dSpriteAnimator spriteAnimator;

	private bool upAnimPlayed;
	private bool downAnimPlayed;

	// Use this for initialization
	override public void Start () {

		base.Start();

		downAnimPlayed = true;
		upAnimPlayed = false;
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {
		
		base.FixedUpdate();

		if( ( ObjectIsRed() && Input.GetKey(KeyCode.LeftShift) ) || 
			( ObjectIsBlue() && Input.GetKey(KeyCode.RightShift) ) )
		{
			flipper.AddForce(Vector3.up * flipperForce);

			if( !upAnimPlayed )
			{
				if( ObjectIsRed() )
					spriteAnimator.Play( "RedFlipperUp" );
				else
					spriteAnimator.Play( "BlueFlipperUp" );

				RefManager.Instance.flipperSFX.Play();
				
				upAnimPlayed = true;
			}
			downAnimPlayed = false;
		}
		else
		{
			flipper.AddForce(Vector3.up * -flipperForce);

			if( !downAnimPlayed )
			{
				if( ObjectIsRed() )
					spriteAnimator.Play( "RedFlipperDown" );
				else
					spriteAnimator.Play( "BlueFlipperDown" );
				
				downAnimPlayed = true;
			}
			upAnimPlayed = false;
		}
	}
}
