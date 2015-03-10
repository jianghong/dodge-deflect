using UnityEngine;
using System.Collections;

public class RespawnIndicator : MonoBehaviour {

	public GameObject playerPrefab;
	public int pNum;
	public int spawnCount;
	GameObject p;
	public Vector3 lerpTarget;
	float timeSpawned;

	// Use this for initialization
	void Start () {
		timeSpawned = Time.time;
		Vector3 spawnPos = new Vector3 (this.transform.position.x, this.transform.position.y-10f, this.transform.position.z);
		p = GameObject.Instantiate(playerPrefab, spawnPos, Quaternion.identity) as GameObject;
		p.GetComponent<MovePlayer>().playerNumber = pNum;
		p.GetComponent<PlayerCollision> ().spawnCount = spawnCount;
		p.GetComponent<MovePlayer>().enabled = false;
		p.GetComponent<PlayerCollision> ().startSpawnImmune();
		lerpTarget = new Vector3(this.transform.position.x, -5f, this.transform.position.z);
		setPlayerLifeText (p);
	}
	
	// Update is called once per frame
	void Update () {
		// lerp to target
		if ((Time.time - timeSpawned) < 0.5f) {
			p.transform.position = Vector3.Lerp(p.transform.position, lerpTarget, 3f * Time.deltaTime * 1f);
		}

		if ((Time.time - timeSpawned) > 1.5f) {
			p.GetComponent<MovePlayer>().enabled = true;
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
			if (liveText.GetComponent<PlayerLivesText>().pNum == pNum) {
				Debug.Log (liveText.GetComponent<PlayerLivesText>().pNum);
				p.GetComponent<PlayerCollision>().lifeText = liveText.GetComponent<PlayerLivesText>();
			}
		}
	}
}
