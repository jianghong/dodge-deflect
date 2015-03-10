using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public float maxTime = 300f;
	public float currTime = 5f;
	bool countdown = false;
	Text text;
	bool timeStopped = true;

	void Awake() {
		text = gameObject.GetComponentInChildren <Text> ();
		text.text = "";
	}
	// Use this for initialization
	void Start () {
//		startTimer ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currTime != 0f) {
			text.text = this.currTime.ToString ("F2");
		}
		if (!timeStopped && countdown) {
			this.currTime -= Time.deltaTime;		
		} else if (!timeStopped && !countdown){
			this.currTime += Time.deltaTime;
		}
	}

	public void startTimer() {
		timeStopped = false;
	}

	public void stopTimer() {
		timeStopped = true;
	}

	public void setCountdown() {
		countdown = true;
	}

	public void setCountup() {
		countdown = false;
	}

	public void resetTimer() {
		currTime = 0f;
	}
}
