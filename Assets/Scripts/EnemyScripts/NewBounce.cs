using UnityEngine;
using System.Collections;

public class NewBounce : MonoBehaviour {
		
	public float initialForce = 500f;
	public float maxVelocity = 50f;
	public float blockerForce = 50f;
	public GameObject BlackHole;
	public GameObject Ball;
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
			Transform shooterPos = collision.gameObject.GetComponentInChildren<Transform>();
			Vector3 newBallPos = new Vector3(shooterPos.position.x, 0.5f, shooterPos.position.z);
			GameObject createdBall = GameObject.Instantiate(Ball, newBallPos, collision.transform.rotation) as GameObject;

			createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*blockerForce, ForceMode.Impulse);

			Destroy(this.gameObject);
		}
		if (Mathf.Abs(this.rigidbody.velocity.x) < maxVelocity) {
			rigidbody.AddForce(rigidbody.velocity.normalized, ForceMode.Impulse);		
		}
	}
}
