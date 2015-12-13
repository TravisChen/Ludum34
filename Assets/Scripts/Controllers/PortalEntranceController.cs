using UnityEngine;
using System.Collections;

public class PortalEntranceController : ObjectController {

	public ParticleSystem particle;

	private tk2dSprite sprite; 
	private SphereCollider sphereCollider;

	private bool particleOn = false;

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
			particle.Stop();
			particleOn = false;
			sphereCollider.enabled = false;
		}
		else if( gameController.IsBlue() && ObjectIsRed() )
		{
			sprite.color = RefManager.Instance.transparentColor;
			particle.Stop();
			particleOn = false;
			sphereCollider.enabled = false;
		}
		else
		{
			sprite.color = Color.white;
			if( !particleOn )
			{
				particle.Play();
				particleOn = true;
			}
			sphereCollider.enabled = true;
		}
	}
}
