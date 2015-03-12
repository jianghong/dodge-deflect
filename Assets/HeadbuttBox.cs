using UnityEngine;
using System.Collections;

public class HeadbuttBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("headbutting other player");
			other.gameObject.GetComponent<ImpactReceiver>().AddImpact(transform.forward, 400f);
		}
	}
}
