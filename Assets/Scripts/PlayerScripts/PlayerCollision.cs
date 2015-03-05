using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	GameManager gameManager;

	public float invinciblityThreshold = 1f;
	public int hitThreshold = 3;
	public GameObject ballPrefab;
	float hitCount = 0.0f;
	float immuneStartTime = 0.0f;
	bool isImmune = false;
	MovePlayer playerMovementScript;
	BallSpawnManager bsm;
	float timeDied;
	public AudioClip playHitClip;
	AudioSource ac;

	void Awake() {
		gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		playerMovementScript = this.GetComponent<MovePlayer> ();
		bsm = GameObject.FindWithTag ("BallSpawnManager").GetComponent<BallSpawnManager> ();
		ac = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		float timeDiff = Time.time - immuneStartTime;
		if(timeDiff >= invinciblityThreshold) {
			isImmune = false;
		}
	}

	void death() {
		int pNum = playerMovementScript.playerNumber;
		gameManager.playerDied (pNum, timeDied);
		int starCounter = 5;
		while (starCounter > 0) {
			GameObject createdBall = GameObject.Instantiate(ballPrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Random.rotation) as GameObject;
			createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*30f, ForceMode.Impulse);
			starCounter -= 1;
		}
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "BlackHole")
		{
			death ();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// If the entering collider is the player...
		if(collision.collider.gameObject.tag == "Ball")
		{
			if(!isImmune && collision.gameObject.GetComponent<NewBounce>().isHostile) {
				bsm.destroyBall(collision.gameObject);
				hitCount += 1f;
				AudioSource.PlayClipAtPoint(playHitClip, this.transform.position);
				immuneStartTime = Time.time;
				// TODO: replace temp fade for health indicator
				gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color /= 2;
				if(hitCount >= hitThreshold) {
					timeDied = Time.time;
					death();
				}
				isImmune = true;
			} 
		}
	}
}
