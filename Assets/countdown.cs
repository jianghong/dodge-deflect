using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countdown : MonoBehaviour {
	MainScene ms;
	Text t;
	public int startTime = 3;
	public int currTime = 3;
	float awakeTime;
	public string startText = "GO!";
	public int roundCount;
	GameManager gm;
	float showRoundDelay = 1.5f;
	bool oneTime = false;
	// Use this for initialization
	void Awake() {
		t = GetComponent<Text> ();
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		ms = GameObject.FindWithTag ("MainSceneManager").GetComponent<MainScene> ();
		Debug.Log ("countdown");
		awakeTime = Time.timeSinceLevelLoad;
	}

	void Start () {
		if (gm.isFinalRound) {
			t.text = "Trophy Round!";
		} else {
			roundCount = 4 - gm.roundCount;
			t.text = "Round " + roundCount.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currTime <= 0 && !oneTime) {
			oneTime = true;
			t.text = startText;
//			t.enabled = false;
			ms.beginGame();
		} else if (!oneTime && (Time.timeSinceLevelLoad - awakeTime) > showRoundDelay) {
			currTime = startTime - Mathf.RoundToInt (Time.timeSinceLevelLoad-showRoundDelay);
			t.text = currTime.ToString ();
		}

	}

	public void Hide() {
		t.enabled = false;
	}
	
}
