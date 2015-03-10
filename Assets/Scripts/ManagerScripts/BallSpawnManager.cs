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
//		this.spawnBall ();
		previousTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
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
		Instantiate(ballPrefab, new Vector3(Random.Range (-16f, 38f), 0.5f, Random.Range (-22f, 41f)), Random.rotation);
	}

	public void destroyBall(GameObject ball) {
		Destroy (ball);
		starCount -= 1;
	}

	public void restartSpawner() {
		this.spawnBall ();
		previousTime = 0f;
	}
}
