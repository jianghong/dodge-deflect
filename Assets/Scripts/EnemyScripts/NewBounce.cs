using UnityEngine;
using System.Collections;

public class NewBounce : MonoBehaviour {
		
	public float initialForce = 20f;
	public float maxVelocity = 50f;
	public GameObject BlackHole;
	public GameObject Ball;
	public float TTL = 0.4f;
	Vector3 shooterPos;
	float spawnTime;
	bool canCollide = false;
	
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		this.rigidbody.AddForce(transform.forward.normalized * initialForce);
	}

	void Update () {
		if ((Time.time - spawnTime) > TTL) {
			canCollide = true;		
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.tag == "Ball" && canCollide) {
			if (this.transform.position.x > collision.transform.position.x) {
				Instantiate (BlackHole, new Vector3 (this.transform.position.x, 0.5f, this.transform.position.z), Quaternion.identity);
			}
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag == "BlackHole" && canCollide) {
			Destroy(this.gameObject);
		}
//
//		if (Mathf.Abs(this.rigidbody.velocity.x) < maxVelocity) {
//			rigidbody.AddForce(rigidbody.velocity.normalized, ForceMode.Impulse);		
//		}
	}
}
