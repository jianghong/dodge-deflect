using UnityEngine;
using System.Collections;

public class BallSpawnManager : MonoBehaviour {

	public GameObject ballPrefab;
	public GameObject[] StaticBallSpawns;
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
	int spawn_i = 0;
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
		GameObject spawnedStar = GameObject.Instantiate (ballPrefab, StaticBallSpawns[spawn_i].transform.position, StaticBallSpawns[spawn_i].transform.rotation) as GameObject;
		spawnedStar.rigidbody.AddForce(StaticBallSpawns[spawn_i].transform.forward*100f, ForceMode.Impulse);
		StaticBallSpawns[spawn_i].GetComponentInParent<Cannon>().bang();
		spawn_i = spawn_i + 1 >= StaticBallSpawns.Length ? 0 : spawn_i + 1;
	
	}

	bool isCloseToPlayer(Vector3 starPos, Vector3 playerPos) {
		float d = Vector3.Distance (starPos, playerPos);
		return (d <= 5f);
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
