using UnityEngine;
using System.Collections;

public class FlipperController : MonoBehaviour {

	public Rigidbody flipper;
	public float flipperForce;
	public bool left;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if( left )
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
