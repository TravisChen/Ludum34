using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class GameController : MonoBehaviour {

	public BallController ball;
	public ProCamera2D proCamera;
	public Transform gameOverTarget;

	private bool gameStarted = false;
	private bool gameOver = false;
	private bool isRed = true;

	private float gameOverTimer;

	private int score = 0;

	private const int COLLECT_POINTS = 100;
	private const float GAME_OVER_TIME = 5.0f;

	// Use this for initialization
	void Start () {
	
		UIManager.Instance.HideUI();
		gameOverTimer = GAME_OVER_TIME;

	}

	public bool IsRed()
	{
		return isRed;
	}

	public bool IsBlue()
	{
		return !isRed;
	}

	private bool UpdateGameStart()
	{
		if( Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) )
		{
			if( !gameStarted )
			{
				proCamera.RemoveAllCameraTargets();
				proCamera.AddCameraTarget( ball.transform, 1, 1 );
				ball.BallActive();
				UIManager.Instance.ShowUI();
				gameStarted = true;
			}
		}

		return gameStarted;
	}

	private bool UpdateGameOver()
	{
		if( Input.GetKeyDown(KeyCode.Q) )
		{
			if( !gameOver )
			{
				proCamera.RemoveAllCameraTargets();
				proCamera.AddCameraTarget( gameOverTarget, 1, 1 );
				UIManager.Instance.HideUI();
				gameOver = true;
			}
		}

		if( gameOver )
		{
			gameOverTimer -= Time.deltaTime;
			if( gameOverTimer <= 0.0f )
			{
				UIManager.Instance.resetText.gameObject.SetActive( true );

				if( Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) )
				{
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}

		return gameOver;
	}

	// Update is called once per frame
	void Update () {

		if( !UpdateGameStart() )
		{
			return;
		}

		if( UpdateGameOver() )
		{
			return;
		}

		UIManager.Instance.scoreText.text = score.ToString();
		UIManager.Instance.endScoreText.text = score.ToString();

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
