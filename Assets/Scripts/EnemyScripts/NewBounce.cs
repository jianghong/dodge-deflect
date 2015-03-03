using UnityEngine;
using System.Collections;

public class NewBounce : MonoBehaviour {
		
	public float initialForce = 20f;
	public float maxVelocity = 50f;
	public GameObject Ball;
	public GameObject voidIndicator;
	public float TTL = 0.4f;
	Vector3 shooterPos;
	float spawnTime;
	int spawnedBy;
	
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		this.rigidbody.AddForce(transform.forward.normalized * initialForce);
	}

	void Update () {
	}

	bool canCollideWithSameSpawnedBy() {
		return ((Time.time - spawnTime) > TTL);
	}

	public void setSpawnedBy(int i) {
		spawnedBy = i;
	}

	public int getSpawnedBy() {
		return spawnedBy;
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.tag == "Ball") {
			if (collision.gameObject.GetComponent<NewBounce>().getSpawnedBy() == spawnedBy) {
				if (!canCollideWithSameSpawnedBy()) {
					return;
				}
			}
			if (this.GetInstanceID() > collision.gameObject.GetInstanceID()) {
				Instantiate (voidIndicator, new Vector3 (this.transform.position.x, -0.6f, this.transform.position.z), Quaternion.identity);
			}
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag == "BlackHole") {
			Destroy(this.gameObject);
		}
	}
}
