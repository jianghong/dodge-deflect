using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject blackholePrefab;
	public GameObject respawnIndicator;
	public GameObject voidIndicatorPrefab;
	public int initialVoidStarCount = 2;
	public int minPlayers;
	public int numPlayers;
	public int[] playersBitmap = {0, 0, 0, 0};
	int[] playersDeflectScore = {0, 0, 0, 0};
	int[] playersHitScore = {0, 0, 0, 0};
	Vector3[] playerPositions = {new Vector3(-11f, -5f, 40f), new Vector3(39f, -5f, 40f), new Vector3(-11f, -5f, -26f), new Vector3(39f, -5f, -26f)};
	Vector3[] respawnposition = {
		new Vector3 (5.46f, -5f, 15.8f),
		new Vector3 (24.63f, -5f, 15.8f),
		new Vector3 (5.46f, -5f, 7.17f),
		new Vector3 (24.32f, -5f, 7.17f)
	};
	bool joiningGame = true;
	bool beginningGame = false;
	bool gameIsOver = false;
	float playerDeathTime;
	ScoreBoard scoreBoardScript;
	WinnerScript ws;


	void Awake() {
		DontDestroyOnLoad (this);
//		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
//		ws = GameObject.FindGameObjectWithTag ("WinnerText").GetComponent<WinnerScript> ();
//		scoreBoardScript = GameObject.FindWithTag ("ScoreBoard").GetComponent<ScoreBoard> ();
//		numPlayers = GameObject.FindGameObjectsWithTag ("Player").Length;
//		for (int i = 0; i < numPlayers; i++) {
//			playersBitmap[i] = 1;
//		}
	}

	// Use this for initialization
	void Start () {
//		timeManager.setCountdown ();
//		timeManager.stopTimer ();
	}
	
	// Update is called once per frame
	void Update () {
		// manages beginning of game
//		if (numPlayers >= minPlayers) {
//			timeManager.startTimer ();		
//		}
//		if (joiningGame) {
//			joinGamePhase ();
//		}
//		if (timeManager.currTime <= 0f) {
//			timeManager.stopTimer();
//			joiningGame = false;
//			beginGame();
//		}
//
//		if ((Time.time - playerDeathTime) > 1f) {
//			Time.timeScale = 1f;		
//		}
//
//		// check game over
//		if (minPlayers <= 1) {
//			if (numPlayers < minPlayers && beginningGame) {
//				gameOver ();
//			}
//		} else {
//			if (numPlayers <= 1 && beginningGame) {
//				ws.setWinnerText (playerLeft());
//				gameOver();		
//			}
//		}
//		restartGame ();
	}
	
	public void addPlayer(int pNum) {
		playersBitmap[pNum-1] = 1;
	}

	void spawnPlayers(int n) {
		for (int i = 0; i < playersBitmap.Length; i++) {
			if (playersBitmap[i] == 1) {
				GameObject respawn = GameObject.Instantiate(respawnIndicator, playerPositions[i], Quaternion.identity) as GameObject;
				respawn.GetComponent<RespawnIndicator>().pNum = i+1;
			}
		}
	}


	public void spawnPlayer(int pNum, int spawnCount) {
		GameObject respawn = GameObject.Instantiate(respawnIndicator, respawnposition[pNum-1], Quaternion.identity) as GameObject;
		RespawnIndicator rs = respawn.GetComponent<RespawnIndicator> ();
		rs.pNum = pNum;
		rs.spawnCount = spawnCount;
	}
	int playerLeft() {
		GameObject[] pNumLeft = GameObject.FindGameObjectsWithTag ("Player");
		if (pNumLeft.Length > 0) {
			return pNumLeft[0].GetComponent<MovePlayer> ().playerNumber;		
		}
		return 0;
	}

	void setPlayerLifeText(GameObject p) {
		GameObject[] livesText = GameObject.FindGameObjectsWithTag("LivesText");
		int pNum = p.GetComponent<MovePlayer> ().playerNumber;
		for (int j = 0; j < livesText.Length; j++) {
			GameObject liveText = livesText[j];
			if (liveText.GetComponent<PlayerLivesText>().pNum == pNum) {
				Debug.Log (liveText.GetComponent<PlayerLivesText>().pNum);
				p.GetComponent<PlayerCollision>().lifeText = liveText.GetComponent<PlayerLivesText>();
			}
		}
	}

	void restartGame () {
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 1))) {
			Application.LoadLevel (Application.loadedLevelName);
		}

		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 2))) {
			Application.LoadLevel (Application.loadedLevelName);
		}

		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 3))) {
			Application.LoadLevel (Application.loadedLevelName);
		}

		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 4))) {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}

	public void incrementScore(int pNum, bool deflectScore) {
		if (deflectScore) {
			playersDeflectScore[pNum-1] += 1;		
		}
		Debug.Log (pNum);
		playersHitScore[pNum-1] += 1;
	}

	public void gameOver () {

		GameObject[] balls = GameObject.FindGameObjectsWithTag ("Ball");
		for(int i = 0; i < balls.Length; i++)
		{
			Destroy(balls[i].gameObject);
		}
		gameIsOver = true;

		scoreBoardScript.setScoreBoard (playersDeflectScore[0], playersHitScore[0],
		                                playersDeflectScore[1], playersHitScore[1],
		                                playersDeflectScore[2], playersHitScore[2],
		                                playersDeflectScore[3], playersHitScore[3]);
	}
}
