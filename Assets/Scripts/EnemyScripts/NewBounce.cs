using UnityEngine;
using System.Collections;

public class NewBounce : MonoBehaviour {
		
	public float initialForce = 500f;
	public float maxVelocity = 50f;
	public GameObject BlackHole;
	public GameObject Ball;
	
	// Use this for initialization
	void Start () {
		this.rigidbody.AddForce(new Vector3(1f, 0, -1f) * initialForce);
	}

	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.tag == "Ball") {
			if (this.transform.position.x > collision.transform.position.x) {
				Instantiate (BlackHole, new Vector3 (this.transform.position.x, 0f, this.transform.position.z), Quaternion.identity);
			}
			Destroy(collision.gameObject);
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag == "BlackHole") {
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag == "Blocker") {
			Instantiate(Ball, new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-10f, 10f)), collision.transform.rotation);
			Destroy(this.gameObject);
		}
		if (Mathf.Abs(this.rigidbody.velocity.x) < maxVelocity) {
			rigidbody.AddForce(rigidbody.velocity.normalized, ForceMode.Impulse);		
			}
		}
	}
