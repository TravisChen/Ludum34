using UnityEngine;
using System.Collections;

public class CollectController : ObjectController {

	private tk2dSprite sprite; 

	// Use this for initialization
	override public void Start () {
	
		base.Start();

		sprite = GetComponent<tk2dSprite>();
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {

		base.FixedUpdate();

		if( gameController.IsRed() && ObjectIsBlue() )
		{
			sprite.color = RefManager.Instance.transparentColor;
		}
		else if( gameController.IsBlue() && ObjectIsRed() )
		{
			sprite.color = RefManager.Instance.transparentColor;
		}
		else
		{
			sprite.color = Color.white;
		}
	}

	public bool CanCollect()
	{
		if( gameController.IsRed() && ObjectIsRed() )
		{
			return true;
		}

		if( gameController.IsBlue() && ObjectIsBlue() )
		{
			return true;
		}

		return false;
	}
}
