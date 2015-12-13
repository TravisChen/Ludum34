using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public ParticleSystem particle;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	public void EnemyHit()
	{
		particle.Play();
		RefManager.Instance.enemyHitSFX.Play();
	}
}
