using UnityEngine;
using System.Collections;

public class RockCentre : MonoBehaviour {

	public float moveSpeed = 1f;
	bool floatUp = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x < 16f) && (transform.position.z > 17f)) {
			floatUp = false;		
		} else if ((transform.position.x > 52f) || (transform.position.z < -36f)) {
			floatUp = true;
		}
		if (floatUp) {
			transform.Translate (new Vector3 (-1f, 0f, 1f) * moveSpeed * Time.deltaTime);
		} else {
			transform.Translate (new Vector3 (1f, 0f, -1f) * moveSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			Debug.Log ("collided player");
		}
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			Debug.Log ("collided player");
		}
	}
}
