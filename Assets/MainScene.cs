using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;
using System.Linq;

public class MainScene : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject respawnIndicator;
	public GameObject voidIndicatorPrefab;
	TimeManager timeManager;
	int[] playersBitmap = {0, 0, 0, 0};
	int[] playersDeflectScore = {0, 0, 0, 0};
	int[] playersHitScore = {0, 0, 0, 0};
	Vector3[] playerPositions = {new Vector3(-11f, -5f, 40f), new Vector3(39f, -5f, 40f), new Vector3(-11f, -5f, -26f), new Vector3(39f, -5f, -26f)};
	Vector3[] respawnposition = {
		new Vector3 (5.46f, -5f, 15.8f),
		new Vector3 (24.63f, -5f, 15.8f),
		new Vector3 (5.46f, -5f, 7.17f),
		new Vector3 (24.32f, -5f, 7.17f)
	};
	bool gameIsOver = false;
	float playerDeathTime;
	ScoreBoard scoreBoardScript;
	WinnerScript ws;

	GameManager gm;
	int numPlayers;
	int minPlayers;
	countdown cd;
	float sceneLoadedTime;
	// Use this for initialization
	void Awake() {
	}
	void Start () {
		sceneLoadedTime = Time.timeSinceLevelLoad;
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
		ws = GameObject.FindGameObjectWithTag ("WinnerText").GetComponent<WinnerScript> ();
		scoreBoardScript = GameObject.FindWithTag ("ScoreBoard").GetComponent<ScoreBoard> ();
		cd = GameObject.FindWithTag ("Countdown").GetComponent<countdown> ();
		numPlayers = gm.playersBitmap.Sum ();
		gm.numPlayers = numPlayers;
		playersBitmap = gm.playersBitmap;
		minPlayers = gm.minPlayers;
		spawnPlayers (numPlayers);
		Debug.Log ("main scene");
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.timeSinceLevelLoad - sceneLoadedTime) > 3.5f) {
			cd.Hide ();
		}

		if (minPlayers <= 1) {
			if (numPlayers < minPlayers) {
				gameOver ();
			}
		} else {
			if (numPlayers <= 1) {
				ws.setWinnerText (playerLeft());
				gameOver();		
			}
		}
		restartGame ();	
	}

	int playerLeft() {
		GameObject[] pNumLeft = GameObject.FindGameObjectsWithTag ("Player");
		if (pNumLeft.Length > 0) {
			return pNumLeft[0].GetComponent<MovePlayer> ().playerNumber;		
		}
		return 0;
	}
	
	public void beginGame() {
		Debug.Log ("n : " + numPlayers);
		Instantiate (voidIndicatorPrefab, new Vector3 (9.9f, 0.5f, 7.3f), Quaternion.identity);
	}

	void spawnPlayers(int n) {
		for (int i = 0; i < playersBitmap.Length; i++) {
			if (playersBitmap[i] == 1) {
				GameObject respawn = GameObject.Instantiate(respawnIndicator, playerPositions[i], Quaternion.identity) as GameObject;
				respawn.GetComponent<RespawnIndicator>().pNum = i+1;
				respawn.GetComponent<RespawnIndicator>().initialSpawn = true;
			}
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
		
		scoreBoardScript.setScoreBoard (playersDeflectScore[0], playersHitScore[0],
		                                playersDeflectScore[1], playersHitScore[1],
		                                playersDeflectScore[2], playersHitScore[2],
		                                playersDeflectScore[3], playersHitScore[3]);
	}

	public void playerDied(int playerNum, float timeDied) {
		numPlayers -= 1;
		playersBitmap [playerNum - 1] = 0;
		Debug.Log ("num players: " + numPlayers);
		playerDeathTime = timeDied;
	}

	public void spawnPlayer(int pNum, int spawnCount) {
		GameObject respawn = GameObject.Instantiate(respawnIndicator, respawnposition[pNum-1], Quaternion.identity) as GameObject;
		RespawnIndicator rs = respawn.GetComponent<RespawnIndicator> ();
		rs.pNum = pNum;
		rs.spawnCount = spawnCount;
	}


	void restartGame () {
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 1))) {
			Destroy(gm);
			Application.LoadLevel ("characterLoadOut");
		}
		
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 2))) {
			Destroy(gm);
			Application.LoadLevel ("characterLoadOut");
		}
		
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 3))) {
			Destroy(gm);
			Application.LoadLevel ("characterLoadOut");
		}
		
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 4))) {
			Destroy(gm);
			Application.LoadLevel ("characterLoadOut");
		}
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Back, 1))) {
			numPlayers = gm.numPlayers;
			Start();
		}
	}
}
