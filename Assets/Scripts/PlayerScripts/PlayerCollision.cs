using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	GameManager gameManager;

	public float invinciblityThreshold = 1f;
	public float hitThreshold = 3f;
	float hitCount = 0.0f;
	float immuneStartTime = 0.0f;
	bool isImmune = false;


	void Awake() {
		gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
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

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == "BlackHole")
		{
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// If the entering collider is the player...
		if(collision.collider.gameObject.tag == "Ball")
		{
			Destroy(collision.gameObject);
			if(!isImmune) {
				hitCount += 1f;
				immuneStartTime = Time.time;
				
				if(hitCount >= hitThreshold) {
					Destroy(this.gameObject);
				}
				
				isImmune = true;
			} 
		}
	}
}
