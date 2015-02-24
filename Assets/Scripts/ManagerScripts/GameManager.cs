using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	TimeManager timeManager;
	BallSpawnManager ballManager;
	PlayerAttributes player;
	bool gameIsOver = false;

	void Awake() {
		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
		ballManager = GameObject.FindWithTag ("BallSpawnManager").GetComponent<BallSpawnManager> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerAttributes> ();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		restartGame ();
	}

	void restartGame () {
		if (gameIsOver && (Input.GetAxisRaw ("Jump") > 0)) {
			timeManager.resetTimer ();
			timeManager.startTimer ();
			ballManager.restartSpawner();
			player.resetPosition();
			gameIsOver = false;
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
