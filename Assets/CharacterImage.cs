using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterImage : MonoBehaviour {

	public int pnum;
	Text t;

	// Use this for initialization
	void Start () {
		t = GetComponentInChildren<Text> ();
		t.text = "Player " + pnum + " press start";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void changeText(string s) {
		t.text = s;
	}

	public void changePanelSprite(Sprite s) {
		transform.FindChild ("CharacterImage").GetComponent<Image> ().enabled = true;
//		GetComponentInChildren<Image> ().enabled = true;
	}	

	public void startPressed(Sprite spr, string str) {
		changeText (str);
		changePanelSprite(spr);
	}
}
