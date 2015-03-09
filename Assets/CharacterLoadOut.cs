using UnityEngine;
using System.Collections;
using System.Linq;
using XboxCtrlrInput;	

public class CharacterLoadOut : MonoBehaviour {

	public Sprite[] characterSprites;
	int maxPlayers = 4;
	float[] players_axisY = {0f, 0f, 0f, 0f};
	int[] players_index = {0, 0, 0, 0};
	int[] playersBitmap = {0, 0, 0, 0};
	bool[] canSwitchCharacterImage = {true, true, true, true};
	CharacterImage[] players_panel = new CharacterImage[4];
	TimeManager tm;
	// Use this for initialization
	void Start () {
		for (int i=0; i < maxPlayers; i++) {
			players_panel[i] = getCharacterPanel (i+1).GetComponent<CharacterImage>();
		}
		tm = GameObject.FindWithTag ("TimeManager").GetComponent<TimeManager> ();
		tm.setCountdown ();
	}
	
	// Update is called once per frame
	void Update () {
		// detect start
		getStartInput (1);
		getStartInput (2);
		getStartInput (3);
		getStartInput (4);
		if (tm.currTime < tm.maxTime || playersBitmap.Sum () == 4) {
			Application.LoadLevel ("scene4");
		}
	}

	void getStartInput(int pNum) {
		if (XCI.GetButtonUp(XboxButton.Start, pNum)) {
			Debug.Log ("p1 pressed start");
			playersBitmap[pNum-1] = 1;
			players_panel[pNum-1].changePanelSprite(characterSprites[pNum-1]);
			players_panel[pNum-1].changeText("Player " + pNum + " joined");
		}
	}
	void getDirectionInput(int pNum) {
		players_axisY[pNum-1] = XCI.GetAxis (XboxAxis.LeftStickY, pNum);
	}

	void changePanel(int pNum) {
		if ((players_axisY[pNum-1] > 0f) && playersBitmap [pNum-1] == 1 && canSwitchCharacterImage [pNum - 1]) {
			// up pressed
			players_panel[pNum-1].changePanelSprite(characterSprites[players_index[pNum-1]]);
			players_index[pNum-1] = (players_index[pNum-1] + 1) > 3 ? 0 : players_index[pNum-1]+1;
		} else if ((players_axisY[pNum-1] < 0f) && playersBitmap [pNum-1] == 1 && canSwitchCharacterImage [pNum - 1]){
			players_panel[pNum-1].changePanelSprite(characterSprites[players_index[pNum-1]]);
			players_index[pNum-1] = (players_index[pNum-1] - 1) < 0 ? 3 : players_index[pNum-1]-1;
		}
		canSwitchCharacterImage [pNum - 1] = false;
		if (players_axisY [pNum - 1] == 0) {
			canSwitchCharacterImage [pNum - 1] = true;
		}
	}
	
	GameObject getCharacterPanel(int pNum) {
		GameObject[] panels = GameObject.FindGameObjectsWithTag ("CharacterImage");
		for (int i=0; i < panels.Length; i++) {
			if (panels[i].GetComponent<CharacterImage>().pnum == pNum) {
				return panels[i];
			}
		}
		return panels [0];
	}
}
