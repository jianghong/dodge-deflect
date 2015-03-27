using UnityEngine;
using System.Collections;

public class StarSpawner : MonoBehaviour {

	public GameObject star;
	float initialForce = 50f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnBall() {
		GameObject spawnedball = GameObject.Instantiate (star, transform.position, transform.rotation) as GameObject;
		Debug.Log (transform.rotation.eulerAngles);
		spawnedball.rigidbody.AddForce(transform.forward.normalized*initialForce, ForceMode.Impulse);
	}
}
