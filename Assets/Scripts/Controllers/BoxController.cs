using UnityEngine;
using System.Collections;

public class BoxController : ObjectController {

	private tk2dSprite sprite; 
	private BoxCollider boxCollider;

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
}
