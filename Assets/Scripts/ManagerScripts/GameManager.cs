﻿using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	TimeManager timeManager;

	bool gameIsOver = false;

	void Awake() {
		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		restartGame ();
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
