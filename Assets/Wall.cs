using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public Material defaultWallMat;
	public Material bounceMat;
	float timeHit;
	float delay = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - timeHit) > delay) {
			setMat (defaultWallMat);
		}
	}

	void setMat(Material m) {
		GetComponent<MeshRenderer> ().material = m;
	}

	void OnCollisionEnter(Collision c) {
		Debug.Log (c.gameObject.tag);
		if (c.gameObject.tag == "Ball") {
			timeHit = Time.time;
			setMat (bounceMat);
		}
	}
}
