using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	Rigidbody rigidbody;
	
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		rigidbody.velocity = new Vector3 (moveHorizontal, moveVertical, 0.0f) * speed;
	}
}
