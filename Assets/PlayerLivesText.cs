using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLivesText : MonoBehaviour {

	public int pNum = 0;
	Text t;	
	// Use this for initialization

	void Start () {
		t = GetComponent<Text> ();
		t.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void setLivesText(int livesLeft) {
		t.text = "P" + pNum + " lives: " + livesLeft;
	}

}
