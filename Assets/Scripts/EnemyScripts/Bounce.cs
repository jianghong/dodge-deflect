using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

	public float initialForce = 500f;
	public float maxVelocity = 50f;

	// Use this for initialization
	void Start () {
		this.rigidbody.AddForce(new Vector3(1f, 0, -1f) * initialForce);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (Mathf.Abs(this.rigidbody.velocity.x) < maxVelocity) {
			rigidbody.AddForce(rigidbody.velocity.normalized, ForceMode.Impulse);		
		}
	}
}
