﻿using UnityEngine;
using System.Collections;
using System.Linq;
using XboxCtrlrInput;	

public class CharacterLoadOut : MonoBehaviour {
	enum ControlType {Auto, Manual}
	public Sprite[] characterSprites;
	GameManager gm;
	int maxPlayers = 4;
	float[] players_axisY = {0f, 0f, 0f, 0f};
	int[] players_index = {0, 0, 0, 0};
	int[] playersBitmap = {0, 0, 0, 0};
	int[] playersReadyState = {0, 0, 0, 0};
	bool[] canSwitchCharacterImage = {true, true, true, true};
	CharacterImage[] players_panel = new CharacterImage[4];
	JoinPrompt jp;
	bool canStartGame = false;


	// Use this for initialization
	void Start () {
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		for (int i=0; i < maxPlayers; i++) {
			players_panel[i] = getCharacterPanel (i+1).GetComponent<CharacterImage>();
		}
		jp = GameObject.FindWithTag ("JoinScreenPrompt").GetComponent<JoinPrompt> ();
	}
	
	// Update is called once per frame
	void Update () {
		// detect start
		getStartInput (1);
		getStartInput (2);
		getStartInput (3);
		getStartInput (4);
		getPickControlInput (1);
		getStartGameInput (1);
		getStartGameInput (2);
		getStartGameInput (3);
		getStartGameInput (4);

		if (playersReadyState.Sum () == gm.minPlayers) {
			canStartGame = true;
			jp.allowStart();	
		}
	}

	void getStartGameInput(int pNum) {
		if (XCI.GetButtonUp(XboxButton.A, pNum) && canStartGame) {
			AutoFade.LoadLevel("scene4", 0.7f, 0.7f, Color.black);
		}		
	}
	void getStartInput(int pNum) {
		if (XCI.GetButtonUp(XboxButton.Start, pNum)) {
			playersBitmap[pNum-1] = 1;
			players_panel[pNum-1].startPressed(characterSprites[pNum-1], "");
			gm.addPlayer(pNum);
		}
	}
	void getPickControlInput(int pNum) {
		bool playerReady = playersReadyState [pNum - 1] == 1;
		if (!playerReady) {
			if (XCI.GetButtonUp (XboxButton.B, pNum)) {
				gm.setPlayerControl (pNum, GameManager.ControlType.Manual);
				players_panel [pNum - 1].controlsPicked ();
				playersReadyState[pNum-1] = 1;
			} else if (XCI.GetButtonUp (XboxButton.X, pNum)) {
				gm.setPlayerControl(pNum, GameManager.ControlType.Auto);
				players_panel[pNum-1].controlsPicked();
				playersReadyState[pNum-1] = 1;
			}
			playerReady = playersReadyState [pNum - 1] == 1;
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
