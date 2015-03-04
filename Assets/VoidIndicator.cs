using UnityEngine;
using System.Collections;

public class VoidIndicator : MonoBehaviour {

	public float timeTilVoid = 1f;
	public GameObject Void;
	float lerpRate = 1f;
	float timeSpawned;
	public Vector3 lerpTargetScale = new Vector3(10f, 0f, 10f);
	// Use this for initialization
	void Start () {
		timeSpawned = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - timeSpawned) > timeTilVoid) {
			Instantiate (Void, new Vector3 (this.transform.position.x, 0.5f, this.transform.position.z), Quaternion.identity);
			Destroy(this.gameObject);
		}

		// lerp to target
		this.transform.localScale = Vector3.Lerp(this.transform.localScale, lerpTargetScale, 3f * Time.deltaTime * lerpRate);
	}
}
