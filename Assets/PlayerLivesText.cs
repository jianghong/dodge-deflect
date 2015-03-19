using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLivesText : MonoBehaviour {

	public int pNum = 0;
	public Color healthColor;
	Image[] healthBlocks;
	RespawnText rt;
	int currHealth = 5;
 
	void Awake() {
		healthBlocks = GetComponentsInChildren<Image> ();
		rt = transform.parent.FindChild ("RespawningText").GetComponent<RespawnText> ();
		for (int i = 0; i < healthBlocks.Length; i++) {
			healthBlocks[i].enabled = false;
		}
	}
	// Use this for initialization

	void Start () {
		Debug.Log ("Pl " + pNum + " started");
		currHealth = 5;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void showRespawningText() {
		Debug.Log (transform.parent.position);
		rt.setText ("Respawning ..");
	}

	public void hideRespawningText() {
		rt.setText ("");
	}

	public void enableHealthBlocks() {
		for (int i = 0; i < healthBlocks.Length; i++) {
			healthBlocks[i].color = healthColor;
			healthBlocks[i].enabled = true;
		}
	}

	public void decreaseHealthBlock(int loseHealth) {
		for (int i = currHealth; i > currHealth-loseHealth; i--) {
			healthBlocks [i].enabled = false;
		}
		currHealth -= loseHealth;
	}

}
