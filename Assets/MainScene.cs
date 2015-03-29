using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;
using System.Linq;

public class MainScene : MonoBehaviour {
	
	public GameObject respawnIndicator;
	public GameObject voidIndicatorPrefab;
	RoundManager rm;
	TimeManager timeManager;
	public int fadeOutTime = 1;
	int[] playersBitmap = {0, 0, 0, 0};
	int[] playersDeflectScore = {0, 0, 0, 0};
	int[] playersHitScore = {0, 0, 0, 0};
	int[] hasBadge = {0, 0, 0, 0};
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
	float gameOverTime;
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
		if (gm.isFinalRound) {
			for (int i = 0; i < gm.playersRoundScore.Length; i++) {
				if (gm.playersRoundScore [i] > 0) {
					playersBitmap [i] = 1;
				} else {
					playersBitmap [i] = 0;
				}
			}
		} else {
			playersBitmap =  (int[])gm.playersBitmap.Clone ();
		}
		numPlayers = playersBitmap.Sum ();
		gm.numPlayers = numPlayers;
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
				int pNumLeft = playerLeft ();
				if (pNumLeft > 0) {
					if (oneTime) {
						oneTime = false;
						GameObject[] pLeft = GameObject.FindGameObjectsWithTag ("Player");
						pLeft[0].GetComponent<PlayerCollision>().addToAvoider();
						gm.tracking.avoider[pNumLeft-1] += 5;
						if (gm.isFinalRound) {
							showScoreBoard(pNumLeft);
							gameOver();
						} else {
							incrementRoundScore(pNumLeft);
							gameOver();
						}
					}
				}
			}
		}
	}

	void showScoreBoard(int pWinner) {
		scoreBoard.SetActive (true);
		float playerLeftSpawnTime = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCollision> ().playerSpawnTime;
		gm.updateLifespan (playerLeft (), Time.time - playerLeftSpawnTime);
		scoreBoard.GetComponent<ScoreBoard> ().disablePlayerContainers (gm.playersBitmap);
		scoreBoard.GetComponent<ScoreBoard>().setLifeSpans (gm.longestLifeSpan, gm.shortestLifeSpan);
		scoreBoard.GetComponent<ScoreBoard> ().displayWinner (pWinner);
		// check for flawless
		if ((pWinner * 3) == gm.roundScores.Sum ()) {
			scoreBoard.GetComponent<ScoreBoard>().setFlawless(pWinner);
		}

		// assign badges
		// TODO: fix edge case for 3 players LOW PRIORITY
		int maxBadge = gm.playersBitmap.Sum () == 4 ? 1 : 2;
		int[] vl = gm.tracking.getStat ("VoidLover");
		int[] hb = gm.tracking.getStat ("Headbutter");
		int[] sh = gm.tracking.getStat ("StarHoarder");
		int[] av = gm.tracking.getStat ("Avoider");
		int[][] stats = {av, vl, hb, sh};
		for (int stat_i=0; stat_i < stats.Length; stat_i++) {
			int badgeVictor = getVictor (stats[stat_i], maxBadge);
			for (int p_i=0; p_i < gm.playersBitmap.Length; p_i++) {
				if (hasBadge[badgeVictor] < maxBadge) {
					if (stats[stat_i][badgeVictor] <= 0) {
						break;
					}
					scoreBoard.GetComponent<ScoreBoard>().assignBadge(badgeVictor, stat_i);
					hasBadge[badgeVictor] += 1;
					break;
				} else {
					stats[stat_i][p_i] = -1;
					badgeVictor = getVictor (stats[stat_i], maxBadge);
					continue;
				}
			}
		}
	}

	public int getVictor(int[] stat, int maxBadge) {
		int victor;
		for (int i=0; i < gm.playersBitmap.Length; i++) {
			victor = stat.ToList().IndexOf(stat.Max());
			if (gm.playersBitmap[victor] == 0 || hasBadge[victor] >= maxBadge ) {
				stat[victor] = -1;
				continue;
			} else {
				return victor;
			}
		}
		return -1;
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
		float playerLeftSpawnTime = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCollision> ().playerSpawnTime;
		gm.updateLifespan (playerLeft (), Time.time - playerLeftSpawnTime);
		roundBoard.SetActive (true);
		roundBoard.transform.FindChild("RowFrame").GetComponent<RoundBoard> ().setRoundVictor ();
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
