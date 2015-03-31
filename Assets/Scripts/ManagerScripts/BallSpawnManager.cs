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
		Vector3 spawnPos = new Vector3 (Random.Range (leftX, rightX), 0.5f, Random.Range (topZ, bottomZ));
		GameObject[] playerObjs = GameObject.FindGameObjectsWithTag ("Player");
		bool canSpawn = false;
		bool canSpawnStatic = true;
//		for (int i=0; i < playerObjs.Length; i++) {
//			if (isCloseToPlayer(spawnPos, playerObjs[i].transform.position)) {
//				canSpawn = false;
//			}
//		}

		if (canSpawn) {
			Instantiate (ballPrefab, spawnPos, Random.rotation);
		} else {
			for (int j=0; j < StaticBallSpawns.Length; j++) {
				canSpawnStatic = true;
				for (int k=0; k < playerObjs.Length; k++) {
					if (spawnPos.Equals(playerObjs[k].transform.position)) {
						canSpawnStatic = false;
						break;
					}
				}
				if (canSpawnStatic) {
					Debug.Log ("Used static spawn");
					Instantiate (ballPrefab, StaticBallSpawns[j].transform.position, -StaticBallSpawns[j].transform.forward);
					break;
				}
			}
		}
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
