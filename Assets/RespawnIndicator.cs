using UnityEngine;
using System.Collections;

public class RespawnIndicator : MonoBehaviour {

	public GameObject playerPrefab;
	public int pNum;
	public int spawnCount;
	public GameManager.ControlType controlType;
	GameObject p;
	public Vector3 lerpTarget;
	float timeSpawned;
	public bool initialSpawn = false;
	MovePlayer mp;
	PlayerCollision pc;
	public int healthSegments;
	public bool isFinalRound;

	// Use this for initialization
	void Start () {
		timeSpawned = Time.time;
		Vector3 spawnPos = new Vector3 (this.transform.position.x, this.transform.position.y-10f, this.transform.position.z);
		p = GameObject.Instantiate(playerPrefab, spawnPos, Quaternion.identity) as GameObject;
		mp = p.GetComponent<MovePlayer> ();
		pc = p.GetComponent<PlayerCollision> ();
		pc.lastHitTime = Time.time;
		pc.playerSpawnTime = Time.time;
		mp.playerNumber = pNum;
		pc.spawnCount = spawnCount;
		pc.startSpawnImmune();
		p.GetComponent<CharacterController>().enabled = false;
		lerpTarget = new Vector3(this.transform.position.x, -5f, this.transform.position.z);
		setPlayerLifeText (p);
		if (isFinalRound) {
			pc.setSpawnCount(healthSegments);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (p.transform.position.x, -2f, p.transform.position.z);
		if (initialSpawn && (Time.time - timeSpawned) > 3f) {
			p.GetComponent<CharacterController>().enabled = true;
		}
		if ((Time.time - timeSpawned) < 0.5f) {
			p.transform.position = Vector3.Lerp(p.transform.position, lerpTarget, 3f * Time.deltaTime * 1f);
		}

		if (!initialSpawn && (Time.time - timeSpawned) > 0.5f) {
			p.GetComponent<CharacterController>().enabled = true;
		}

		if ((Time.time - timeSpawned) > p.GetComponent<PlayerCollision>().spawnInvin) {
			Destroy(this.gameObject);
		}
	}

	public GameObject getPlayer() {
		return p;
	}


	void setPlayerLifeText(GameObject p) {
		GameObject[] livesText = GameObject.FindGameObjectsWithTag("LivesText");
		int pNum = p.GetComponent<MovePlayer> ().playerNumber;
		for (int j = 0; j < livesText.Length; j++) {
			GameObject liveText = livesText[j];
			if (liveText.GetComponentInChildren<PlayerLivesText>().pNum == pNum) {
				p.GetComponent<PlayerCollision>().lifeText = liveText.GetComponent<PlayerLivesText>();
				liveText.GetComponent<PlayerLivesText>().enableHealthBlocks();
				if (isFinalRound) {
					liveText.GetComponent<PlayerLivesText>().decreaseRespawnCount(healthSegments.ToString());
				}
			}
		}
	}
}
