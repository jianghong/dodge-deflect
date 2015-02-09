using UnityEngine;
using System.Collections;

public class BallSpawnManager : MonoBehaviour {

	public GameObject ballPrefab;
	TimeManager timeManager;
	float currTime;
	float previousTime;

	void Awake() {
		timeManager = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
	}

	// Use this for initialization
	void Start () {
		this.spawnBall ();
		previousTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		currTime = timeManager.currTime;
		if ((currTime - previousTime) > 7f) {
			spawnBall ();
			previousTime = currTime;
		}
	}
	
	void spawnBall() {
		Instantiate(ballPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
	}

	public void restartSpawner() {
		this.spawnBall ();
		previousTime = 0f;
	}
}
