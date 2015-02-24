using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public GameObject ballPrefab;
	public float starCounter = 2f;
	public float spawnTime = 0f;

	void Start() {
		spawnTime = Time.time;
	}

	void Update() {
		if (Time.time > spawnTime + 5) {
			while (starCounter > 0) {
				Instantiate(ballPrefab, this.transform.position, Random.rotation);
				starCounter -= 1;
			}
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.collider.gameObject.tag == "Ball") {
			starCounter += 1;
			Destroy (other.gameObject);
		}
	}
}
