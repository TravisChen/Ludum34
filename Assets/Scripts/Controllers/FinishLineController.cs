using UnityEngine;
using System.Collections;

public class FinishLineController : ObjectController {

	private BoxCollider boxCollider;

	private bool boostSFXPlayed = false;

	private const float BOOST_FORCE = 4000.0f;

	// Use this for initialization
	override public void Start () {
	
		base.Start();

		boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	override public void FixedUpdate () {

		base.FixedUpdate();
	}

	void OnTriggerEnter(Collider other) {

		if( other.tag == "Ball" )
		{
			gameController.PlayerFinish();
		}
	}

	void OnTriggerExit(Collider other) {
		
		boostSFXPlayed = false;
	}
}
