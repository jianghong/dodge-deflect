using UnityEngine;
using System.Collections;

public class HeadbuttBox : MonoBehaviour {

	int pNum;
	int otherpNum;
	MovePlayer mp;
	// Use this for initialization
	void Start () {
		mp = this.GetComponentInParent<MovePlayer> ();
		Debug.Log (mp);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			pNum = mp.playerNumber;
			otherpNum = other.gameObject.GetComponent<MovePlayer> ().playerNumber;
			if (otherpNum != pNum) {
				Debug.Log ("headbutting other player");
				other.gameObject.GetComponent<ImpactReceiver>().AddImpact(transform.forward, 400f);
			}
		}
	}
}
