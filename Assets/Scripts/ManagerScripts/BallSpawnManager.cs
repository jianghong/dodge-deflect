using UnityEngine;
using System.Collections;
using System.Linq;

public class BallSpawnManager : MonoBehaviour {

	public GameObject ballPrefab;
	public float spawnInterval = 1.3f;
	float currTime;
	float previousTime;
	public int starCount;
	public int starLimit = 7;
	GameObject spawnCoords;
	Transform rss;
	Transform lss;
	GameObject TopLeft;
	GameObject BottomRight;
	float leftX, rightX;
	float topZ, bottomZ;
	GameManager gm;
	bool rightSpawnRotateLeft = false;
	bool leftSpawnRotateLeft = false;
	float rotateSpeed = 1f;
	public float initialForce = 500f;
	bool useRightSpawn = true;

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
		rss = spawnCoords.transform.Find ("RightStarSpawner");
		lss = spawnCoords.transform.Find ("LeftStarSpawner");
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

		if (gm.playersBitmap.Sum () == 2) {
			spawnInterval = 1.3f;
		} else {
			spawnInterval = 1.5f;
		}

		rotateRightSpawner ();
		rotateLeftSpawner ();
	}

	void rotateRightSpawner (){
		float leftBound = 0f;
		float rightBound = 180f;
		float r = rss.rotation.eulerAngles.y;

		if (r <= 360f && r >= 200f) {
			rightSpawnRotateLeft = false;
		} else if (r >= 180f) {
			rightSpawnRotateLeft = true;
		}

		if (rightSpawnRotateLeft) {
			rss.Rotate (new Vector3 (0, -rotateSpeed, 0));
		} else {
			rss.Rotate (new Vector3 (0, rotateSpeed, 0));
		}
	}

	void rotateLeftSpawner (){
		float leftBound = 0f;
		float rightBound = 180f;
		float r = lss.rotation.eulerAngles.y;
		
		if (r >= 0f && r <= 160f ) {
			leftSpawnRotateLeft = true;
		} else if (r <= 180f) {
			leftSpawnRotateLeft = false;
		}

		Debug.Log (r);
		if (leftSpawnRotateLeft) {
			lss.Rotate (new Vector3 (0, -rotateSpeed, 0));
		} else {
			lss.Rotate (new Vector3 (0, rotateSpeed, 0));
		}
	}
	
	void spawnBall() {
		starCount += 1;
		if (useRightSpawn) {
			rss.GetComponent<StarSpawner> ().spawnBall ();
			useRightSpawn = false;
		} else {
			lss.GetComponent<StarSpawner> ().spawnBall ();
			useRightSpawn = true;
		}


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
