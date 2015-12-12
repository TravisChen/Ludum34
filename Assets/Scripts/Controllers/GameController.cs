using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class GameController : MonoBehaviour {

	private bool isRed = true;

	private int score = 0;

	private const int COLLECT_POINTS = 100;

	// Use this for initialization
	void Start () {
	
	}

	public bool IsRed()
	{
		return isRed;
	}

	public bool IsBlue()
	{
		return !isRed;
	}

	// Update is called once per frame
	void Update () {

		UIManager.Instance.scoreText.text = score.ToString();

		if( Input.GetKeyDown(KeyCode.LeftShift) )
		{
			isRed = true;
		}

		if( Input.GetKeyDown(KeyCode.RightShift) )
		{
			isRed = false;
		}

	}

	public void Collect()
	{
		score += COLLECT_POINTS;

		ProCamera2DShake.Instance.ShakeUsingPreset("Collect");
	}
}
