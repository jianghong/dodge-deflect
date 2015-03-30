using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {
	public enum ControlType {Auto, Manual}
	public int minPlayers;
	public int numPlayers;
	public int[] playersBitmap = {0, 0, 0, 0};
	public int[] playersRoundScore = {0, 0, 0, 0};
	public int[] roundScores = {0, 0, 0, 0};
	public ControlType[] playerControls = {ControlType.Manual, ControlType.Manual, ControlType.Manual, ControlType.Manual};
	public float[] longestLifeSpan = {0f, 0f, 0f, 0f};
	public float[] shortestLifeSpan = {Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, Mathf.Infinity};
	public int roundCount = 3;
	public bool isFinalRound = false;
	public StatTracking tracking;

	void Awake() {
		DontDestroyOnLoad (this);
		tracking = GetComponent<StatTracking> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void addPlayer(int pNum) {
		playersBitmap[pNum-1] = 1;
	}

	public void setPlayerControl(int pNum, ControlType controlType) {
		playerControls [pNum - 1] = controlType;
	}

	public void finalRound() {
		isFinalRound = true;
		AutoFade.LoadLevel("try_large_scene", 0.7f, 0.7f, Color.black);
	}

	void resetParameters() {

	}

	public void incrementRoundScore(int pNum) {
		playersRoundScore [pNum - 1] += 1;
		roundScores [3 - roundCount] = pNum;
	}
	
	public void newRound() {
		if (roundCount > 1) {
			roundCount -= 1;
			AutoFade.LoadLevel("try_large_scene", 0.7f, 0.7f, Color.black);
		} else if (roundCount == 1 && !isFinalRound) {
			finalRound ();
		} else {
			AutoFade.LoadLevel("characterLoadOut", 0.7f, 0.7f, Color.black);
			Destroy (this);			
		}
	}

	public void updateLifespan(int pNum, float timeValue) {
		if ((timeValue < shortestLifeSpan [pNum - 1]) && timeValue >= 3f) {
			shortestLifeSpan[pNum-1] = timeValue;
		}
		
		if (timeValue > longestLifeSpan [pNum - 1]) {
			longestLifeSpan[pNum-1] = timeValue;
		}
	}
}
