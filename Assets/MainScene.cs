using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;
using System.Linq;

public class MainScene : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject respawnIndicator;
	public GameObject voidIndicatorPrefab;
	RoundManager rm;
	TimeManager timeManager;
	public int fadeOutTime = 1;
	int[] playersBitmap = {0, 0, 0, 0};
	int[] playersDeflectScore = {0, 0, 0, 0};
	int[] playersHitScore = {0, 0, 0, 0};
	bool[] hasBadge = {false, false, false, false};
	Vector3[] playerPositions = new Vector3[4];
	Vector3[] respawnposition = new Vector3[4];
	GameObject[] playersHUD;
	bool gameIsOver = false;
	float playerDeathTime;
	bool oneTime = true;
	GameManager gm;
	int numPlayers;
	int minPlayers;
	countdown cd;
	float sceneLoadedTime;
	GameObject spawnCoords;
	int pleft = 0;
	float deathDelay = 2.7f;
	public GameObject roundBoard;
	public GameObject scoreBoard;

	// Use this for initialization
	void Awake() {
	}

	void Start () {
		sceneLoadedTime = Time.timeSinceLevelLoad;
		playersHUD = GameObject.FindGameObjectsWithTag("LivesText");
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
		cd = GameObject.FindWithTag ("Countdown").GetComponent<countdown> ();
		numPlayers = gm.playersBitmap.Sum ();
		gm.numPlayers = numPlayers;
		playersBitmap =  (int[])gm.playersBitmap.Clone ();
		minPlayers = gm.minPlayers;
		spawnCoords = GameObject.FindWithTag ("SpawnCoords");
		respawnposition [0] = spawnCoords.transform.Find ("P1Spawn").transform.position;
		respawnposition [1] = spawnCoords.transform.Find ("P2Spawn").transform.position;
		respawnposition [2] = spawnCoords.transform.Find ("P3Spawn").transform.position;
		respawnposition [3] = spawnCoords.transform.Find ("P4Spawn").transform.position;
		playerPositions = respawnposition;

		spawnPlayers (numPlayers);
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
				if (playerLeft () > 0) {
					if (oneTime) {
						oneTime = false;
						if (gm.isFinalRound) {
							showScoreBoard();
						} else {
							incrementRoundScore(playerLeft ());
							gameOver();
						}
					}
				}
			}
		}
		restartGame ();
	}

	void showScoreBoard() {
		scoreBoard.SetActive (true);
		float playerLeftSpawnTime = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCollision> ().playerSpawnTime;
		gm.updateLifespan (playerLeft (), Time.time - playerLeftSpawnTime);
		scoreBoard.GetComponent<ScoreBoard> ().disablePlayerContainers (gm.playersBitmap);
		scoreBoard.GetComponent<ScoreBoard>().setLifeSpans (gm.longestLifeSpan, gm.shortestLifeSpan);

		// assign badges

		int[] vl = gm.tracking.getStat ("VoidLover");
		int[] hb = gm.tracking.getStat ("Headbutter");
		int[] sh = gm.tracking.getStat ("StarHoarder");
		int[] av = gm.tracking.getStat ("Avoider");
		int[][] stats = {vl, hb, sh, av};
		for (int i=0; i < stats.Length; i++) {
			int max = stats[i].Max ();
			int badgeVictor =  stats[i].ToList ().IndexOf (max);
			if (i == gm.playersBitmap.Sum()) {
				for (int j=0; j < hasBadge.Length; j ++) {
					hasBadge[j] = false;
				}
			}
			for (int k=0; k<gm.playersBitmap.Length; k++) {
				max = stats[i].Max ();
				badgeVictor =  stats[i].ToList ().IndexOf (max);
				if (!hasBadge[badgeVictor]) {
					scoreBoard.GetComponent<ScoreBoard>().assignBadge(badgeVictor, i);
					hasBadge[badgeVictor] = true;
				} else {
					stats[i][badgeVictor] = -1;
				}
			}
		}
	}

	int playerLeft() {
		GameObject[] pNumLeft = GameObject.FindGameObjectsWithTag ("Player");
		if (pNumLeft.Length > 0) {
			return pNumLeft[0].GetComponent<MovePlayer> ().playerNumber;		
		}
		return -1;
	}

	public void incrementRoundScore(int pNum) {
		gm.incrementRoundScore (pNum);
		roundBoard.SetActive (true);
		roundBoard.transform.FindChild("Row").GetComponent<RoundBoard> ().setRoundVictor ();
	}


	public void beginGame() {
		Debug.Log ("n : " + numPlayers);
	}

	void spawnPlayers(int n) {
		RespawnIndicator rs;
		for (int i = 0; i < playersBitmap.Length; i++) {
			if (playersBitmap[i] == 1) {
				GameObject respawn = GameObject.Instantiate(respawnIndicator, playerPositions[i], Quaternion.identity) as GameObject;
				rs = respawn.GetComponent<RespawnIndicator>();
				rs.pNum = i+1;
				rs.initialSpawn = true;
				rs.controlType = gm.playerControls[i];
				if (gm.isFinalRound){
					rs.isFinalRound = true;
					rs.healthSegments = gm.playersRoundScore[i];
				}
			}
		}
		for (int j= 0; j < 4; j++) {
			if (playersBitmap[playersHUD[j].GetComponent<PlayerLivesText>().pNum-1] == 1) {
				playersHUD[j].GetComponent<PlayerLivesText>().enableHealthBlocks ();
			} else {
				playersHUD[j].transform.parent.gameObject.SetActive(false);
			}
		}
	}

	public void gameOver () {
		gameIsOver = true;
	}

	public void playerDied(int playerNum, float timeDied) {
		numPlayers -= 1;
		playersBitmap [playerNum - 1] = 0;
		Debug.Log ("num players: " + numPlayers);
	}

	public void spawnPlayerStarter(int pNum, int spawnCount) {
		StartCoroutine (spawnPlayer (pNum, spawnCount));
		getPlayerHUD(pNum).GetComponent<PlayerLivesText> ().showRespawningText ();
	}

	GameObject getPlayerHUD(int pNum) {
		for (int j= 0; j < 4; j++) {
			if (playersHUD [j].GetComponent<PlayerLivesText> ().pNum == pNum) {
				return playersHUD [j];
			}
		}
		return playersHUD [0];
	}

	IEnumerator spawnPlayer(int pNum, int spawnCount) {
		yield return new WaitForSeconds (deathDelay);
		float playerDeath = Time.time;
		GameObject respawn = GameObject.Instantiate(respawnIndicator, respawnposition[pNum-1], Quaternion.identity) as GameObject;
		RespawnIndicator rs = respawn.GetComponent<RespawnIndicator> ();
		rs.pNum = pNum;
		rs.spawnCount = spawnCount;
		rs.controlType = gm.playerControls [pNum - 1];
		getPlayerHUD(pNum).GetComponent<PlayerLivesText> ().hideRespawningText ();
	}

	public void incrementScore(int pNum, bool deflectScore) {
		if (deflectScore) {
			playersDeflectScore[pNum-1] += 1;		
		}
		playersHitScore[pNum-1] += 1;
	}
	
	void restartGame () {
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 1))) {
			gm.newRound();
		}
		
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 2))) {
			gm.newRound();
		}
		
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 3))) {
			gm.newRound();
		}
		
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 4))) {
			gm.newRound();
		}
		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Back, 1))) {
			numPlayers = gm.numPlayers;
			Start();
		}
	}
}
