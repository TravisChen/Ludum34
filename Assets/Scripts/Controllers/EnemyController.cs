using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	public void EnemyHit()
	{
		RefManager.Instance.enemyHitSFX.Play();
	}
}
