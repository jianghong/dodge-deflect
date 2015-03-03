using UnityEngine;
using System.Collections;

public class StarDespawnPoint : MonoBehaviour {

	BallSpawnManager bsm;
	// Use this for initialization
	void Start () {
		bsm = GameObject.FindWithTag ("BallSpawnManager").GetComponent<BallSpawnManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ball") {
			bsm.destroyBall (collision.gameObject);
		}
	}
}
