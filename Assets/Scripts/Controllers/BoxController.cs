using UnityEngine;
using System.Collections;

public class BoxController : ObjectController {

	public tk2dSpriteAnimator animator; 
	private BoxCollider boxCollider;

	private bool upAnimPlayed;
	private bool downAnimPlayed;

	// Use this for initialization
	override public void Start () {
	
		base.Start();

		boxCollider = GetComponent<BoxCollider>();

		downAnimPlayed = true;
		upAnimPlayed = false;
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {

		base.FixedUpdate();

		if( gameController.IsRed() && ObjectIsBlue() )
		{
			if( !downAnimPlayed )
			{
				animator.Play( "BlueWallDown" );
				downAnimPlayed = true;
			}
			upAnimPlayed = false;

			boxCollider.enabled = false;
		}
		else if( gameController.IsBlue() && ObjectIsRed() )
		{
			if( !downAnimPlayed )
			{
				animator.Play( "RedWallDown" );
				downAnimPlayed = true;
			}
			upAnimPlayed = false;

			boxCollider.enabled = false;
		}
		else
		{
			if( !upAnimPlayed )
			{
				if( ObjectIsRed() )
				{
					animator.Play( "RedWallUp" );
				}
				else
				{
					animator.Play( "BlueWallUp" );
				}
				upAnimPlayed = true;
			}
			downAnimPlayed = false;
			boxCollider.enabled = true;
		}
	}
}
