using UnityEngine;
using System.Collections;

public class VoidIndicator : MonoBehaviour {

	public float timeTilVoid = 1f;
	public GameObject Void;
	float timeSpawned;
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
	}
}
