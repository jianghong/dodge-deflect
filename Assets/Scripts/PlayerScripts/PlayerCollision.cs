using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
	
	MainScene ms;
	public float invinciblityThreshold = 0.5f;
	public float defaultInvin = 0.5f;
	public float spawnInvin = 3f;
	public int hitThreshold = 3;
	public GameObject ballPrefab;
	public int maxSpawnCount = 3;
	public int spawnCount = 0;
	float hitCount = 0.0f;
	float immuneStartTime = 0.0f;
	bool isImmune = false;
	MovePlayer playerMovementScript;
	BallSpawnManager bsm;
	float timeDied;
	Animator animator;
	public AudioClip playHitClip;
	public PlayerLivesText lifeText;
	LionelDeflect ld;

	void Awake() {
		ms = GameObject.FindWithTag ("MainSceneManager").GetComponent<MainScene> ();
		playerMovementScript = this.GetComponent<MovePlayer> ();
		bsm = GameObject.FindWithTag ("BallSpawnManager").GetComponent<BallSpawnManager> ();
	}
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		ld = GetComponentInChildren<LionelDeflect> ();
	}


	// Update is called once per frame
	void Update () {
		float timeDiff = Time.time - immuneStartTime;
		if(timeDiff >= invinciblityThreshold) {
			isImmune = false;
			invinciblityThreshold = defaultInvin;
		}
	}

	void death() {
		int pNum = playerMovementScript.playerNumber;
		int starCounter = 2;
		while (starCounter > 0) {
			GameObject createdBall = GameObject.Instantiate(ballPrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Random.rotation) as GameObject;
			createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*30f, ForceMode.Impulse);
			starCounter -= 1;
		}
		spawnCount += 1;
		Debug.Log ("death: " + spawnCount);
		if (spawnCount < maxSpawnCount) {
			ms.spawnPlayer (pNum, spawnCount);
		} else {
			ms.playerDied (pNum, timeDied);
		}

		Destroy (this.gameObject);
	}

	public void startSpawnImmune() {
		invinciblityThreshold = spawnInvin;
		immuneStartTime = Time.time;
		isImmune = true;
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "BlackHole" && !isImmune)
		{
			if (hitCount == 1.0f) {
				lifeText.decreaseHealthBlock(1);
			} else {
				lifeText.decreaseHealthBlock(2);
			}
			death ();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// If the entering collider is the player...
		if(collision.collider.gameObject.tag == "Ball")
		{
			NewBounce collidedStar = collision.gameObject.GetComponent<NewBounce>();
			if(!isImmune && collidedStar.isHostile) {
				bsm.destroyBall(collision.gameObject);
				// increment hit count
				if ((collidedStar.shotByPNum) > -1) {
					Debug.Log ("hit by p: " + collidedStar.shotByPNum);
					if (collidedStar.getDeflectedStar()) {
						Debug.Log ("hit by deflected");
					}

					ms.incrementScore(collidedStar.shotByPNum, collidedStar.getDeflectedStar());
				}
				ld.triggerIsHit();
				hitCount += 1f;
				animator.SetTrigger("isHit");
				lifeText.decreaseHealthBlock(1);
				AudioSource.PlayClipAtPoint(playHitClip, this.transform.position);
				immuneStartTime = Time.time;
				if(hitCount >= hitThreshold) {
					timeDied = Time.time;
					death();
				}
				isImmune = true;
			} 
		}
	}
}
