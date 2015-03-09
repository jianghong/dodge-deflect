using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterImage : MonoBehaviour {

	public int pnum;
	ShowHide[] arrows;

	// Use this for initialization
	void Start () {
		arrows = GetComponentsInChildren<ShowHide>();
		for (int i = 0; i < arrows.Length; i++) {
			arrows[i].Hide();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
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
