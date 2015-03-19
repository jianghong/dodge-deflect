using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RespawnText : MonoBehaviour {

	Text t;
	// Use this for initialization
	void Start () {
		t = GetComponent<Text> ();
		t.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void setText(string s) {
		t.text = s;
	}
}
