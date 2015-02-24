using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
	
	GameManager gameManager;
	
	private float healthCount = 0.0f;
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
		if(timeDiff >= 5.0f) {
			isImmune = false;
		}
	}

	void OnTriggerEnter(Collider collision)
	{
	
		// If the entering collider is the player...
		if(collision.collider.gameObject.tag == "Ball")
		{
			if(!isImmune) {
				healthCount += 1.0f;
				immuneStartTime = Time.time;
				
				if(healthCount >= 3) {
					Destroy(this.gameObject);
				}
				
				isImmune = true;
				immuneStartTime = Time.time;
			} 
		}
		
		if(collision.collider.gameObject.tag == "BlackHole")
		{
			Destroy(this.gameObject);
		}
	}
}
