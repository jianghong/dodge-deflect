using UnityEngine;
using System.Collections;

public class BallSpawnManager : MonoBehaviour {

	public GameObject ballPrefab;
	public float spawnInterval = 7f;
	TimeManager timeManager;
	float currTime;
	float previousTime;
	public int starCount;
	public int starLimit = 5;

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
		Debug.Log ("Star count: " + starCount);
		currTime = timeManager.currTime;
		if ((currTime - previousTime) > spawnInterval && starCount < starLimit) {
//			Vector3 randDirection = new Vector3 (0f, Random.Range(0f, 360f), 0f);
//			this.gameObject.transform.LookAt (randDirection);
			spawnBall ();
			previousTime = currTime;
		}
	}
	
	void spawnBall() {
		starCount += 1;
		Instantiate(ballPrefab, new Vector3(Random.Range (-17f, 17f), 0.5f, Random.Range (-12f, 12f)), Random.rotation);
	}

	public void restartSpawner() {
		this.spawnBall ();
		previousTime = 0f;
	}
}
