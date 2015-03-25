using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLivesText : MonoBehaviour {

	public int pNum = 0;
	RawImage[] healthBlocks;
	RespawnText rt;
	int currHealth = 0;
	PlayerPortrait playerPortrait;
 
	void Awake() {
		playerPortrait = transform.parent.Find ("Portrait").GetComponent<PlayerPortrait> ();
		healthBlocks = GetComponentsInChildren<RawImage> ();
		rt = transform.parent.FindChild ("RespawningText").GetComponent<RespawnText> ();
		for (int i = 0; i < healthBlocks.Length; i++) {
			healthBlocks[i].enabled = false;
		}
	}
	// Use this for initialization

	void Start () {
		Debug.Log ("Pl " + pNum + " started");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void showRespawningText() {
		rt.setText ("Respawning ..");
	}

	public void hideRespawningText() {
		rt.setText ("");
	}

	public void enableHealthBlocks() {
		for (int i = 0; i < healthBlocks.Length; i++) {
			healthBlocks[i].enabled = true;
		}
		currHealth = 0;
	}

	public void disableHealthBlocks() {
		for (int i = 0; i < healthBlocks.Length; i++) {
			healthBlocks[i].enabled = false;
		}
		currHealth = 0;
	}

	public void decreaseHealthBlock(int loseHealth) {
		if (loseHealth == 1) {
			healthBlocks [currHealth].enabled = false;
			currHealth = currHealth + loseHealth > 1 ? 0 : 1;
		} else {
			disableHealthBlocks();
		}
//		playerPortrait.changePortrait ("hit");
	}

	public void decreaseRespawnCount(string s) {
		GetComponentInChildren<ChangeText> ().setText (s);
	}
}
