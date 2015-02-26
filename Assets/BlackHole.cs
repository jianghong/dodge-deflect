using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public GameObject ballPrefab;
	public float starCounter = 2f;
	public float spawnTime = 0f;
	public float duration = 5f;
	BallSpawnManager ballSpawnManager;
	
	void Start() {
		spawnTime = Time.time;
	}

	void Update() {
		if (Time.time > spawnTime + duration) {
			while (starCounter > 0) {
				GameObject createdBall = GameObject.Instantiate(ballPrefab, this.transform.position, Random.rotation) as GameObject;
				createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*30f, ForceMode.Impulse);
				starCounter -= 1;
			}
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("bh:" + other.gameObject.tag);
		if (other.collider.gameObject.tag == "Ball") {
			starCounter += 1;
			this.transform.localScale *= 1.20f;
			Destroy (other.gameObject);
		}
	}
}
