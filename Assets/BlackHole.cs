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
				Instantiate(ballPrefab, new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-10f, 10f)), Quaternion.identity);
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
