﻿using UnityEngine;
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
		GetComponent<Image>().color = new Color(0.29f, 0.61f, 0f);
		t.text = "Press A to start game";
	}

	public void disallowStart() {
		GetComponent<Image>().color = new Color(1.0f, 78/255, 78/255);
		t.text = "Waiting for players";
	}
}
