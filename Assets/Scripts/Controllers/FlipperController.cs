using UnityEngine;
using System.Collections;

public class FlipperController : ObjectController {

	public Rigidbody flipper;
	public float flipperForce;

	// Use this for initialization
	override public void Start () {

		base.Start();
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {
		
		base.FixedUpdate();

		if( objectIsRed )
		{
			if( Input.GetKey(KeyCode.LeftShift) )
			{
				flipper.AddForce(Vector3.up * flipperForce);
			}
			else
			{
				flipper.AddForce(Vector3.up * -flipperForce);
			}
		}
		else
		{
			if( Input.GetKey(KeyCode.RightShift) )
			{
				flipper.AddForce(Vector3.up * flipperForce);
			}
			else
			{
				flipper.AddForce(Vector3.up * -flipperForce);
			}
		}
	}
}
