using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	public bool objectIsRed;
	public GameController gameController {get;set;}

	// Use this for initialization
	public virtual void Start () {
		gameController = GameObject.FindGameObjectWithTag( "GameController" ).GetComponent<GameController>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	public virtual void FixedUpdate() {
	}

	public bool ObjectIsRed()
	{
		return objectIsRed;
	}

	public bool ObjectIsBlue()
	{
		return !objectIsRed;
	}
}
