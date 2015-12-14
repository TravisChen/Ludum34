using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RefManager : MonoBehaviour {

	public GameObject uiCamera;
	public TextMesh scoreText;
	public TextMesh timeText;
	public TextMesh endScoreText;
	public TextMesh endTimeText;

	public GameObject redCollectParticle;
	public GameObject blueCollectParticle;
	public GameObject enemyHitParticle;

	public Material redTrailMat;
	public Material blueTrailMat;

	public AudioSource song;
	public AudioSource portalSFX;
	public AudioSource flipperSFX;
	public AudioSource collectSFX;
	public AudioSource wallHitSFX;
	public AudioSource boostSFX;
	public AudioSource enemyHitSFX;
	public AudioSource startSFX;
	public AudioSource finishSFX;

	public Transform progressMarker;
	public Transform progressMeter;
	public float progressMarkerLowY;
	public float progressMarkerHighY;
	public Transform scoreBackground;

	public tk2dSpriteAnimator redOrbLord;
	public tk2dSpriteAnimator blueOrbLord;

	public Color transparentColor;

	// Static singleton property
	public static RefManager Instance { get; private set; }

	void Start()
	{
	}

	void Update()
	{

	}

	public void ShowUI()
	{
		scoreText.gameObject.SetActive( true );
		timeText.gameObject.SetActive( true );
		progressMeter.gameObject.SetActive( true );
		progressMarker.gameObject.SetActive( true );
		scoreBackground.gameObject.SetActive( true );
	}

	public void HideUI()
	{
		scoreText.gameObject.SetActive( false );
		timeText.gameObject.SetActive( false );
		progressMeter.gameObject.SetActive( false );
		progressMarker.gameObject.SetActive( false );
		scoreBackground.gameObject.SetActive( false );
	}

	void Awake()
	{
		AwakeInstance();
	}

	void AwakeInstance()
	{
		// First we check if there are any other instances conflicting
		if(Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}

		// Here we save our singleton instance
		Instance = this;

		// Furthermore we make sure that we don't destroy between scenes (this is optional)
		DontDestroyOnLoad(gameObject);
	}
}