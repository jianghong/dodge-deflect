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
		// init players bitmap for final round
		for (int i = 0; i < playersRoundScore.Length; i++) {
			if (playersRoundScore[i] > 0) {
				playersBitmap[i] = 1;
			} else {
				playersBitmap[i] = 0;
			}
		}
		isFinalRound = true;
		Application.LoadLevel ("scene4");

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
			Application.LoadLevel ("scene4");
		} else if (roundCount == 1 && !isFinalRound) {
			finalRound ();
		} else {
			Application.LoadLevel("characterLoadOut");
			Destroy (this);			
		}
	}
}
