using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countdown : MonoBehaviour {
	MainScene ms;
	TimeManager tm;
	Text t;
	public int startTime = 6;
	public int currTime = 6;
	bool oneTime = false;
	// Use this for initialization
	void Start () {
		tm = GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>();
		t = GetComponent<Text> ();
		ms = GameObject.FindWithTag ("MainSceneManager").GetComponent<MainScene> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currTime <= 0 && !oneTime) {
			oneTime = true;
			t.enabled = false;
			ms.beginGame();
			t.text = "";
		} else {
			currTime = startTime - Mathf.RoundToInt (Time.time);
			t.text = currTime.ToString ();
		}

	}
	
}
