using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	bool rightSpawnRotateLeft = false;
	bool leftSpawnRotateLeft = false;
	float rotateSpeed = 0.5f;
	public float leftBound = 310f;
	public float rightBound = 203f;
	bool stop;
	float stopDelay = 1f;
	float bangTime;
	Animator ac;
	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ac = GetComponent<Animator> ();
		ps = transform.Find ("HoldParticles").GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!stop) {
			rotateRightSpawner ();
		}
		if ((Time.time - bangTime) >= stopDelay) {
			stop = false;
		}
	}

	
	void rotateRightSpawner (){

		float r = gameObject.transform.rotation.eulerAngles.y;
		
		if (r <= rightBound && r >= rightBound-20f) {
			rightSpawnRotateLeft = false;
		} else if (r >= leftBound) {
			rightSpawnRotateLeft = true;
		}
		
		if (rightSpawnRotateLeft) {
			gameObject.transform.Rotate (new Vector3 (0, -rotateSpeed, 0));
		} else {
			gameObject.transform.Rotate (new Vector3 (0, rotateSpeed, 0));
		}
	}

	IEnumerator startBang(GameObject spawnObj, GameObject ballPrefab) {
		yield return new WaitForSeconds (stopDelay);
		GameObject spawnedStar = GameObject.Instantiate (ballPrefab, spawnObj.transform.position, spawnObj.transform.rotation) as GameObject;
		spawnedStar.rigidbody.AddForce(spawnObj.transform.forward*100f, ForceMode.Impulse);
		ps.Stop ();
		ac.SetTrigger ("bang");
	}

	public void bang(GameObject spawnObj, GameObject ballPrefab) {
		ps.Play ();
		StartCoroutine (startBang (spawnObj, ballPrefab));
		stop = true;
		bangTime = Time.time;
	}
}
