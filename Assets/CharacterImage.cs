using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterImage : MonoBehaviour {

	public int pnum;
	ShowHide[] arrows;
	Text t;

	// Use this for initialization
	void Start () {
		arrows = GetComponentsInChildren<ShowHide>();
		for (int i = 0; i < arrows.Length; i++) {
			arrows[i].Hide();
		}
		t = GetComponentInChildren<Text> ();
		t.text = "Player " + pnum + " press start";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeText(string s) {
		t.text = s;
	}

	public void changePanelSprite(Sprite s) {
		GetComponent<Image> ().sprite = s;
	}

	public void showArrows() {
		arrows = GetComponentsInChildren<ShowHide>(includeInactive:true);
		Debug.Log (arrows.Length);
		for (int i = 0; i < arrows.Length; i++) {
			arrows[i].Show();
		}
	}
}
