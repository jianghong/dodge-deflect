using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public enum ControlType {Auto, Manual}
	public int minPlayers;
	public int numPlayers;
	public int[] playersBitmap = {0, 0, 0, 0};
	public ControlType[] playerControls = {ControlType.Manual, ControlType.Manual, ControlType.Manual, ControlType.Manual};
	public int roundCount = 3;

	void Awake() {
		DontDestroyOnLoad (this);
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

	public void newRound() {
		if (roundCount > 1) {
			roundCount -= 1;
			Application.LoadLevel("scene4");
		} 
	}
}
