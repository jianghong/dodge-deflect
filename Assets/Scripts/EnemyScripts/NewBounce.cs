using UnityEngine;
using System.Collections;

public class NewBounce : MonoBehaviour {
		
	public float initialForce = 500f;
	public float maxVelocity = 50f;
	public float blockerForce = 50f;
	public GameObject BlackHole;
	public GameObject Ball;
	Vector3 shooterPos;
	float spawnTime;
	bool canCollide = false;
	
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
//		this.rigidbody.AddForce(transform.forward.normalized * initialForce);
	}

	void Update () {
		if ((Time.time - spawnTime) > 0.5f) {
			canCollide = true;		
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Blocker") {
			
			Component[] trans = collider.gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform tran in trans) {
				shooterPos = tran.position;
			}
			Vector3 newBallPos = new Vector3(shooterPos.x, 0.5f, shooterPos.z);
			GameObject createdBall = GameObject.Instantiate(Ball, newBallPos, collider.transform.rotation) as GameObject;
			createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*blockerForce, ForceMode.Impulse);
			Destroy(this.gameObject);
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

		if (Mathf.Abs(this.rigidbody.velocity.x) < maxVelocity) {
			rigidbody.AddForce(rigidbody.velocity.normalized, ForceMode.Impulse);		
		}
	}
}
