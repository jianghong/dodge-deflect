using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject blackholePrefab;
	public int initialVoidStarCount = 2;
	public int minPlayers;
	TimeManager timeManager;
	int numPlayers;
	int[] playersBitmap = {0, 0, 0, 0};
	Vector3[] playerPositions = {new Vector3(-11f, -5f, 17f), new Vector3(27f, -5f, 17f), new Vector3(25f, -5f, -13f), new Vector3(-9f, 0.5f, -13f)};
	bool joiningGame = true;
	bool beginningGame = false;
	bool gameIsOver = false;
	float playerDeathTime;

	void Awake() {
		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
		numPlayers = GameObject.FindGameObjectsWithTag ("Player").Length;
		for (int i = 0; i < numPlayers; i++) {
			playersBitmap[i] = 1;
		}
	}

	// Use this for initialization
	void Start () {
		timeManager.currTime = 1f;
		timeManager.setCountdown ();
		timeManager.stopTimer ();
	}
	
	// Update is called once per frame
	void Update () {
		// manages beginning of game
		if (numPlayers >= minPlayers) {
			timeManager.startTimer ();		
		}
		if (joiningGame) {
			joinGamePhase ();
		}
		if (timeManager.currTime <= 0f) {
			timeManager.stopTimer();
			joiningGame = false;
			beginGame();
		}

		if ((Time.time - playerDeathTime) > 1f) {
			Time.timeScale = 1f;		
		}

		// check game over
		if (minPlayers <= 1) {
			if (numPlayers < minPlayers && beginningGame) {
				gameOver ();
			}
		} else {
			if (numPlayers <= 1 && beginningGame) {
				gameOver();		
			}
		}
		restartGame ();
	}

	void joinGamePhase() {
		if (XCI.GetButtonUp(XboxButton.Start, 1) && (playersBitmap[0] == 0)) {
			numPlayers += 1;
			playersBitmap[0] = 1;
			Debug.Log("p1 joined");
		}
		if (XCI.GetButtonUp(XboxButton.Start, 2) && (playersBitmap[1] == 0)) {
			numPlayers += 1;
			playersBitmap[1] = 1;
			Debug.Log("p2 joined");
		}
		if (XCI.GetButtonUp(XboxButton.Start, 3) && (playersBitmap[2] == 0)) {
			numPlayers += 1;
			playersBitmap[2] = 1;
			Debug.Log("p3 joined");
		}
		if (XCI.GetButtonUp(XboxButton.Start, 4) && (playersBitmap[3] == 0)) {
			numPlayers += 1;
			playersBitmap[3] = 1;
			Debug.Log("p4 joined");
		}
	}

	void beginGame() {
		beginningGame = true;
		timeManager.startTimer ();
		timeManager.currTime = 0f;
		timeManager.setCountup ();
		Debug.Log ("num players: " + numPlayers);
		spawnPlayers (numPlayers);
		GameObject initVoid = GameObject.Instantiate (blackholePrefab, new Vector3 (6f, 0.5f, 0f), Quaternion.identity) as GameObject;
		initVoid.GetComponent<BlackHole>().starCounter = initialVoidStarCount;
		initVoid.GetComponent<BlackHole> ().scaleToStarCount ();
	}


	void spawnPlayers(int n) {
		for (int i = 0; i < playersBitmap.Length; i++) {
			if (playersBitmap[i] == 1) {
				GameObject p = GameObject.Instantiate(playerPrefab, playerPositions[i], Quaternion.identity) as GameObject;
				p.GetComponent<MovePlayer>().playerNumber = i+1;
			}
		}
	}

	public void playerDied(int playerNum, float timeDied) {
		numPlayers -= 1;
		playersBitmap [playerNum - 1] = 0;
		Debug.Log ("num players: " + numPlayers);
		playerDeathTime = timeDied;
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

	public void gameOver () {
		timeManager.stopTimer ();

		GameObject[] balls = GameObject.FindGameObjectsWithTag ("Ball");
		for(int i = 0; i < balls.Length; i++)
		{
			Destroy(balls[i].gameObject);
		}
		gameIsOver = true;
	}
}
