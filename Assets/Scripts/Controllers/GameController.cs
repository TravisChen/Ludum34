using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int score = 0;

	private const int COLLECT_POINTS = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		UIManager.Instance.scoreText.text = score.ToString();

	}

	public void Collect()
	{
		score += COLLECT_POINTS;
	}
}
