using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;

public class startScene : MonoBehaviour {

	public Sprite[] startLoopFrames;
	public Sprite[] startEndFrames;
	public Sprite[] startEndLoopFrames;
	public string sceneToLoad;
	public int fadeOutTime = 2;
	int startLoopI = 0;
	int startEndI = 0;
	int startEndLoopI = 0;
	bool startLoopDone = false;
	bool transitionDone = false;
	float frameDelay = 0.10f;
	float lastFrameTime;
	bool startPressed = false;
	bool oneTime = false;
	AudioManager audioM;
	public AudioClip bgm;
	// Use this for initialization
	void Start () {
		audioM = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager> ();
		audioM.setClip (bgm, 0f, 0f);
		lastFrameTime = Time.time;	
	}
	
	// Update is called once per frame
	void Update () {
		if (!startPressed) {
			getStartInput (1);
			getStartInput (2);
			getStartInput (3);
			getStartInput (4);
			loopFrames(startLoopFrames, ref startLoopI);
		} else if (!transitionDone) {
			transitionScene ();
		} else {
			loopFrames(startEndLoopFrames, ref startEndLoopI);
			if ((startEndLoopI == (startEndLoopFrames.Length - 1)) && !oneTime) {
				oneTime = true;
				AutoFade.LoadLevel(sceneToLoad, fadeOutTime, 1, Color.black);
			}
		}
	}

	void getStartInput(int pNum) {
		if (XCI.GetButtonUp(XboxButton.Start, pNum)) {
			startPressed = true;
		}
	}

	void transitionScene () {
		if (startLoopI == (startLoopFrames.Length -1)) {
			loopFrames(startLoopFrames, ref startLoopI);
			startLoopDone = true;
		}
		if (!startLoopDone) {
			loopFrames(startLoopFrames, ref startLoopI);
		}
		if (((Time.time - lastFrameTime) > frameDelay) && (!transitionDone)) {
			setFrame (startEndFrames[startEndI]);
			startEndI += 1;
			lastFrameTime = Time.time;
			if (startEndI == startEndFrames.Length-1) {
				transitionDone = true;
			}
		}
	}
	
	void setFrame(Sprite s) {
		GetComponent<Image> ().sprite = s;
	}

	void loopFrames(Sprite[] frames, ref int i) {
		if ((Time.time - lastFrameTime) > frameDelay) {
			setFrame (frames [i]);
			i = (i + 1) >= frames.Length ? 0 : i + 1;
			lastFrameTime = Time.time;
		}
	}
}
