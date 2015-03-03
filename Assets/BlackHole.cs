using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public GameObject ballPrefab;
	public GameObject bhPrefab;
	public int starCounter = 2;
	public float spawnTime = 0f;
	public float duration = 5f;
	public float maxScale = 20f;
	BallSpawnManager ballSpawnManager;
	
	void Start() {
		spawnTime = Time.time;
		for (int i = 0 ;(i < (starCounter-2)); i++) {
			this.transform.localScale *= 1.25f;
		}
	}

	void Update() {
		if (Time.time > spawnTime + duration) {
			while (starCounter > 0) {
				GameObject createdBall = GameObject.Instantiate(ballPrefab, this.transform.position, Random.rotation) as GameObject;
				createdBall.GetComponent<NewBounce>().setSpawnedBy(GetInstanceID());
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
			this.transform.localScale *= 1.25f;
			duration -= 0.5f;
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "BlackHole") {
			if (this.GetInstanceID() > other.GetInstanceID()) {
				Debug.Log ("Creating super BH");
				GameObject createdVoid = GameObject.Instantiate (bhPrefab, new Vector3 (this.transform.position.x, 0.5f, this.transform.position.z), Quaternion.identity) as GameObject;
				createdVoid.GetComponent<BlackHole>().starCounter += starCounter;
			}
			Destroy(this.gameObject);
		}
	}
}
