using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinPrompt : MonoBehaviour {

	Text t;
	// Use this for initialization
	void Start () {
		t = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void allowStart() {
		GetComponent<Image>().color = new Color(0, 255f, 0);
		t.text = "Press A to start game";
	}
}
