using UnityEngine;
using System.Collections;

public class PortalEntranceController : ObjectController {

	private tk2dSprite sprite; 
	private SphereCollider sphereCollider;

	// Use this for initialization
	override public void Start () {
	
		base.Start();

		sphereCollider = GetComponent<SphereCollider>();
		sprite = GetComponent<tk2dSprite>();
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {

		base.FixedUpdate();

		if( gameController.IsRed() && ObjectIsBlue() )
		{
			sprite.color = RefManager.Instance.transparentColor;
			sphereCollider.enabled = false;
		}
		else if( gameController.IsBlue() && ObjectIsRed() )
		{
			sprite.color = RefManager.Instance.transparentColor;
			sphereCollider.enabled = false;
		}
		else
		{
			sprite.color = Color.white;
			sphereCollider.enabled = true;
		}
	}
}
