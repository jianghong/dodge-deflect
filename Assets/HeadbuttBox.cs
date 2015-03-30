using UnityEngine;
using System.Collections;

public class HeadbuttBox : MonoBehaviour {

	int pNum;
	int otherpNum;
	MovePlayer mp;
	GameManager gm;
	// Use this for initialization
	void Start () {
		mp = this.GetComponentInParent<MovePlayer> ();
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
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
				gm.incrementScore(pNum);
				other.gameObject.GetComponent<ImpactReceiver>().AddImpact(transform.forward, 400f);
			}
		}
	}
}
