using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countdown : MonoBehaviour {
	MainScene ms;
	TimeManager tm;
	Text t;
	public int startTime = 3;
	public int currTime = 3;
	public string startText = "GO!";
	bool oneTime = false;
	// Use this for initialization
	void Awake() {
		tm = GameObject.FindWithTag("TimeManager").GetComponent<TimeManager>();
		t = GetComponent<Text> ();
		ms = GameObject.FindWithTag ("MainSceneManager").GetComponent<MainScene> ();
		Debug.Log ("countdown");
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (currTime <= 0 && !oneTime) {
			oneTime = true;
			t.text = startText;
//			t.enabled = false;
			ms.beginGame();
		} else if (!oneTime) {
			currTime = startTime - Mathf.RoundToInt (Time.timeSinceLevelLoad);
			t.text = currTime.ToString ();
		}

	}

	public void Hide() {
		t.enabled = false;
	}
	
}
