using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class GameController : MonoBehaviour {

	public BallController ball;
	public ProCamera2D proCamera;
	public Transform gameOverTarget;

	private bool playerFinish = false;
	private bool gameStarted = false;
	private bool gameOver = false;
	private bool isRed = true;

	private float gameOverTimer;

	private int score = 0;
	private float totalTime = 0.0f;

	private const int COLLECT_POINTS = 100;
	private const int ENEMY_HIT_POINTS = 1000;
	private const float GAME_OVER_TIME = 5.0f;

	private bool redOrbLordPlayed = false;
	private bool blueOrbLordPlayed = false;

	// Use this for initialization
	void Start () {
	
		RefManager.Instance.HideUI();
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
				RefManager.Instance.ShowUI();
				RefManager.Instance.song.Play();
				RefManager.Instance.startSFX.Play();
				gameStarted = true;
			}
		}

		return gameStarted;
	}

	private bool UpdateGameOver()
	{
		if( playerFinish )
		{
			if( !gameOver )
			{
				proCamera.RemoveAllCameraTargets();
				proCamera.AddCameraTarget( gameOverTarget, 1, 1 );
				RefManager.Instance.HideUI();
				RefManager.Instance.song.Stop();
				RefManager.Instance.finishSFX.Play();
				gameOver = true;
			}
		}

		if( gameOver )
		{
			gameOverTimer -= Time.deltaTime;
			if( gameOverTimer <= 0.0f )
			{
				RefManager.Instance.resetText.gameObject.SetActive( true );

				if( Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) )
				{
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}

		return gameOver;
	}

	private void UpdateOrbLords()
	{
		if( Input.GetKeyDown(KeyCode.LeftShift) )
		{
			isRed = true;

			if( !redOrbLordPlayed )
			{
				RefManager.Instance.redOrbLord.StopAndResetFrame();
				RefManager.Instance.redOrbLord.Play( "RedOrbLord" );
				OrbLordSmash();
				redOrbLordPlayed = true;
			}
		}
		else
		{
			redOrbLordPlayed = false;
		}

		if( Input.GetKeyDown(KeyCode.RightShift) )
		{
			isRed = false;

			if( !blueOrbLordPlayed )
			{
				RefManager.Instance.blueOrbLord.StopAndResetFrame();
				RefManager.Instance.blueOrbLord.Play( "BlueOrbLord" );
				OrbLordSmash();
				blueOrbLordPlayed = true;
			}
		}
		else
		{
			blueOrbLordPlayed = false;
		}
	}

	// Update is called once per frame
	void Update () {

		UpdateOrbLords();

		if( !UpdateGameStart() )
		{
			return;
		}

		if( UpdateGameOver() )
		{
			return;
		}

		totalTime += Time.deltaTime;
		int seconds = (int)( totalTime % 60.0f );
		int minutes = (int)( totalTime / 60 );

		string timeString;
		if( seconds < 10 )
		{
			timeString = minutes + ":" + "0" + seconds.ToString();
		}
		else
		{
			timeString = minutes + ":" + seconds.ToString();
		}

		RefManager.Instance.timeText.text = timeString;
		RefManager.Instance.endTimeText.text = timeString;

		RefManager.Instance.scoreText.text = score.ToString();
		RefManager.Instance.endScoreText.text = score.ToString();

	}

	public void PlayerFinish()
	{
		if( !playerFinish )
		{
			playerFinish = true;
		}
	}

	public bool IsGameOver()
	{
		return gameOver;
	}

	public void OrbLordSmash()
	{
		ProCamera2DShake.Instance.ShakeUsingPreset("OrbLordSmash");
	}

	public void EnemyHit()
	{
		score += ENEMY_HIT_POINTS;

		ProCamera2DShake.Instance.ShakeUsingPreset("EnemyHit");
	}

	public void WallHit()
	{
		ProCamera2DShake.Instance.ShakeUsingPreset("WallHit");
	}

	public void Collect()
	{
		score += COLLECT_POINTS;
	}
}
