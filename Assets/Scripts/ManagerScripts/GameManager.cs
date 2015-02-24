using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

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

		if (gameIsOver && (XCI.GetButtonUp(XboxButton.Start, 2))) {
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
