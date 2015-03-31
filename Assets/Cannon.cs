using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	bool rightSpawnRotateLeft = false;
	bool leftSpawnRotateLeft = false;
	float rotateSpeed = 0.5f;
	public float leftBound = 310f;
	public float rightBound = 203f;
	Animator ac;

	// Use this for initialization
	void Start () {
		ac = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		rotateRightSpawner ();
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

	public void bang() {
		ac.SetTrigger ("bang");
	}
}
