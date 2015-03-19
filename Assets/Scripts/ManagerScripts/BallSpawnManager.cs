using UnityEngine;
using System.Collections;

public class BallSpawnManager : MonoBehaviour {

	public GameObject ballPrefab;
	public float spawnInterval = 7f;
	float currTime;
	float previousTime;
	public int starCount;
	public int starLimit = 7;
	GameObject spawnCoords;
	GameObject TopLeft;
	GameObject BottomRight;
	float leftX, rightX;
	float topZ, bottomZ;
	GameManager gm;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		spawnCoords = GameObject.FindWithTag ("SpawnCoords");
		leftX = spawnCoords.transform.Find("TopLeft").transform.position.x;
		rightX = spawnCoords.transform.Find("BottomRight").transform.position.x;
		topZ = spawnCoords.transform.Find("TopLeft").transform.position.z;
		bottomZ = spawnCoords.transform.Find("BottomRight").transform.position.z;
		previousTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		currTime = Time.time;
		if ((currTime - previousTime) > spawnInterval && starCount < starLimit) {
			spawnBall ();
			previousTime = currTime;
		}
		if (gm.isFinalRound) {
			starLimit = 14;
		}
	}
	
	void spawnBall() {
		starCount += 1;
		Instantiate(ballPrefab, new Vector3(Random.Range (leftX, rightX), 0.5f, Random.Range (topZ, bottomZ)), Random.rotation);
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
