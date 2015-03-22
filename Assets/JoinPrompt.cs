using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinPrompt : MonoBehaviour {

	public Sprite readyBox;
	public Sprite unreadyBox;
	Text t;
	// Use this for initialization
	void Start () {
		t = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void allowStart() {
		GetComponent<Image> ().sprite = readyBox;
		t.text = "HEADBUTT (TAP LT) TO START!";
	}

	public void disallowStart() {
		GetComponent<Image> ().sprite = unreadyBox;
		t.text = "Waiting for players";
	}
}
