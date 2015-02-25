﻿using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	TimeManager timeManager;
	int numPlayers;
	int[] playersBitmap = {0, 0, 0, 0};
	int maxPlayers = 4;
	bool joiningGame = true;
	bool beginningGame = false;

	bool gameIsOver = false;

	void Awake() {
		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
		numPlayers = GameObject.FindGameObjectsWithTag ("Player").Length;
		for (int i = 0; i < numPlayers; i++) {
			playersBitmap[i] = 1;
		}
	}

	// Use this for initialization
	void Start () {
		timeManager.currTime = 5f;
		timeManager.setCountdown ();
		timeManager.startTimer ();
	}
	
	// Update is called once per frame
	void Update () {

		if (joiningGame) {
			joinGamePhase ();
		}
		if (timeManager.currTime <= 0f) {
			Debug.Log (timeManager.currTime);
			timeManager.stopTimer();
			joiningGame = false;
			beginGame();
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
		Debug.Log("Game Over");
		timeManager.stopTimer ();

		GameObject[] balls = GameObject.FindGameObjectsWithTag ("Ball");
		for(int i = 0; i < balls.Length; i++)
		{
			Destroy(balls[i].gameObject);
		}
		gameIsOver = true;
	}
}
