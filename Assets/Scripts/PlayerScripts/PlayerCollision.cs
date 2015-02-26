﻿using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	GameManager gameManager;

	public float invinciblityThreshold = 1f;
	public float hitThreshold = 3f;
	float hitCount = 0.0f;
	float immuneStartTime = 0.0f;
	bool isImmune = false;
	MovePlayer playerMovementScript;
	BallSpawnManager bsm;


	void Awake() {
		gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		playerMovementScript = this.GetComponent<MovePlayer> ();
		bsm = GameObject.FindWithTag ("BallSpawnManager").GetComponent<BallSpawnManager> ();
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
		gameManager.playerDied (pNum);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "BlackHole")
		{
			death ();
		}
//		if (collider.gameObject.tag == "Ball") {
//			Debug.Log ("player collided with ball");
//		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// If the entering collider is the player...
		if(collision.collider.gameObject.tag == "Ball")
		{
			Debug.Log ("taking damage");
			bsm.destroyBall(collision.gameObject);
			if(!isImmune) {

				hitCount += 1f;
				immuneStartTime = Time.time;
				
				if(hitCount >= hitThreshold) {
					death();
				}
				
				isImmune = true;
			} 
		}
	}
}
