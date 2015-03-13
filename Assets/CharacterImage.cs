using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterImage : MonoBehaviour {

	public int pnum;
	Text t;
	ShowHide pickControl;
	ShowHide readyRow;

	// Use this for initialization
	void Start () {
		t = GetComponentInChildren<Text> ();
		t.text = "Player " + pnum + " press start";
		pickControl = transform.Find ("PickControl").gameObject.GetComponent<ShowHide> ();
		readyRow = transform.Find ("ReadyRow").gameObject.GetComponent<ShowHide> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void changeText(string s) {
		t.text = s;
	}

	public void changePanelSprite(Sprite s) {
		GetComponent<Image> ().sprite = s;
	}	

	public void startPressed(Sprite spr, string str) {
		changeText (str);
		changePanelSprite(spr);
		pickControl.Show ();
	}

	public void controlsPicked() {
		pickControl.Hide ();
		readyRow.Show ();
	}
}
