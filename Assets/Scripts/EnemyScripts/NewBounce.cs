using UnityEngine;
using System.Collections;

public class NewBounce : MonoBehaviour {
		
	public float initialForce = 500f;
	public float maxVelocity = 50f;
	public GameObject BlackHole;
	public GameObject Ball;
	float spawnTime;
	bool canCollide = false;
	
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
//		Debug.Log ("forward: " + transform.forward.normalized);
		this.rigidbody.AddForce(transform.forward.normalized * initialForce);
	}

	void Update () {
		if ((Time.time - spawnTime) > 0.5f) {
			canCollide = true;		
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.tag == "Ball" && canCollide) {
			if (this.transform.position.x > collision.transform.position.x) {
				Instantiate (BlackHole, new Vector3 (this.transform.position.x, 0f, this.transform.position.z), Quaternion.identity);
			}
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag == "BlackHole" && canCollide) {
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag == "Blocker") {
			Vector3 newBallPos = new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);
			Instantiate(Ball, newBallPos, collision.transform.rotation);
			Destroy(this.gameObject);
		}
		if (Mathf.Abs(this.rigidbody.velocity.x) < maxVelocity) {
			rigidbody.AddForce(rigidbody.velocity.normalized, ForceMode.Impulse);		
		}
	}
}
