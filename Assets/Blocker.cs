using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {

	public GameObject ballPrefab;

	void OnTriggerEnter(Collider collision) {
		if (collision.collider.gameObject.tag == "Ball") {
			Destroy(collision.gameObject);
			Instantiate(ballPrefab, new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-10f, 10f)), Quaternion.identity);
		}
	}
}
