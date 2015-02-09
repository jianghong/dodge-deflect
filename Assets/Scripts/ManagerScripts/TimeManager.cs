using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public float maxTime = 300f;
	public float currTime = 0f;
	Text text;
	bool timeStopped = true;

	void Awake() {
		text = gameObject.GetComponentInChildren <Text> ();
	}
	// Use this for initialization
	void Start () {
		startTimer ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = this.currTime.ToString ("F2");
		if (!timeStopped) {
			this.currTime += Time.deltaTime;		
		}
	}

	public void startTimer() {
		timeStopped = false;
	}

	public void stopTimer() {
		timeStopped = true;
	}

	public void resetTimer() {
		currTime = 0f;
	}
}
