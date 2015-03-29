using UnityEngine;
using System.Collections;
using System.Linq;
using XboxCtrlrInput;
using UnityEngine.UI;

public class CharacterLoadOut : MonoBehaviour {
	enum ControlType {Auto, Manual}
	public Sprite[] holdSprites;
	public Sprite[] releaseSprites;
	public Sprite[] headbuttSprites;
	public Sprite[] idleSprites;
	public GameObject promptText;
	float countdownStart;
	GameManager gm;
	int maxPlayers = 4;
	float[] players_axisY = {0f, 0f, 0f, 0f};
	int[] players_index = {0, 0, 0, 0};
	int[] playersBitmap = {0, 0, 0, 0};
	int[] playersReadyState = {0, 0, 0, 0};
	int[] readiedUp = {0, 0, 0, 0};
	bool[] canSwitchCharacterImage = {true, true, true, true};
	CharacterImage[] players_panel = new CharacterImage[4];
	JoinPrompt jp;
	bool canStartGame = false;
	ChangeText minPlayerCount;
	
	// Use this for initialization
	void Awake () {
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		for (int i=0; i < maxPlayers; i++) {
			players_panel[i] = getCharacterPanel (i+1).GetComponent<CharacterImage>();
		}
		jp = GameObject.FindWithTag ("JoinScreenPrompt").GetComponent<JoinPrompt> ();
		minPlayerCount = GameObject.FindWithTag ("MinPlayerCount").GetComponent<ChangeText> ();
		minPlayerCount.setText (gm.minPlayers.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		// detect start
		getStartInput (1);
		getStartInput (2);
		getStartInput (3);
		getStartInput (4);
		getPickControlInput (1);
		getPickControlInput (2);
		getPickControlInput (3);
		getPickControlInput (4);
		getStartGameInput (1);
		getStartGameInput (2);
		getStartGameInput (3);
		getStartGameInput (4);
		getChangeMinPlayerInput (1);
		if (playersReadyState.Sum () >= gm.minPlayers) {
			canStartGame = true;
			jp.allowStart ();	
		} else {
			canStartGame = false;
			jp.disallowStart();
		}
		countdownToNextScene ();
	}

	void getStartGameInput(int pNum) {
		if (playersReadyState [pNum - 1] == 1) {
			if (((XCI.GetAxis(XboxAxis.LeftTrigger, pNum) > 0) && (XCI.GetAxis(XboxAxis.LeftTrigger, pNum) != 0.5f)) && canStartGame) {
				players_panel[pNum-1].startPressed(headbuttSprites[pNum-1], "");
				readiedUp[pNum-1] = 1;
				if (readiedUp.Sum() == playersBitmap.Sum()) {
					countdownStart = Time.time;
				}
			}
		}
	}

	void getStartInput(int pNum) {
		if ((XCI.GetAxis(XboxAxis.RightTrigger, pNum) > 0) && (XCI.GetAxis(XboxAxis.RightTrigger, pNum) != 0.5f)) {
			playersBitmap[pNum-1] = 1;
			players_panel[pNum-1].startPressed(holdSprites[pNum-1], "Release RT!");
			gm.addPlayer(pNum);
			playersReadyState[pNum-1] = 1;
		}

		if ((XCI.GetAxis (XboxAxis.RightTrigger, pNum) <= 0f) && playersReadyState[pNum-1] == 1) {
			players_panel[pNum-1].startPressed(releaseSprites[pNum-1], "");
		}
	}


	void countdownToNextScene() {
		if (readiedUp.Sum () == gm.minPlayers) {
			float timer = (10f - (Time.time - countdownStart));
			promptText.GetComponent<Text>().text = "Game starting in " + timer.ToString("F0");
			StartCoroutine(goToGame());
		}
	}

	IEnumerator goToGame() {
		yield return new WaitForSeconds (9);
		AutoFade.LoadLevel("try_large_scene", 0.7f, 0.7f, Color.black);
	}

	void getPickControlInput(int pNum) {
		bool playerReady = playersReadyState [pNum - 1] == 1;
		if (!playerReady) {
			if (XCI.GetButtonUp (XboxButton.B, pNum)) {
				gm.setPlayerControl (pNum, GameManager.ControlType.Manual);
				playersReadyState[pNum-1] = 1;
			} else if (XCI.GetButtonUp (XboxButton.X, pNum)) {
				gm.setPlayerControl(pNum, GameManager.ControlType.Auto);
				playersReadyState[pNum-1] = 1;
			}
			playerReady = playersReadyState [pNum - 1] == 1;
		}
	}

	void getDirectionInput(int pNum) {
		players_axisY[pNum-1] = XCI.GetAxis (XboxAxis.LeftStickY, pNum);
	}

	void getChangeMinPlayerInput(int pNum) {
		if (XCI.GetButtonDown(XboxButton.LeftBumper, pNum)) {
			gm.minPlayers = Mathf.Max(1, --gm.minPlayers);
			minPlayerCount.setText(gm.minPlayers.ToString());
		} else if (XCI.GetButtonDown(XboxButton.RightBumper, pNum)) {
			gm.minPlayers = Mathf.Min (4, ++gm.minPlayers);
			minPlayerCount.setText(gm.minPlayers.ToString());
		}
	}

	void changePanel(int pNum) {
		if ((players_axisY[pNum-1] > 0f) && playersBitmap [pNum-1] == 1 && canSwitchCharacterImage [pNum - 1]) {
			// up pressed
			players_panel[pNum-1].changePanelSprite(holdSprites[players_index[pNum-1]]);
			players_index[pNum-1] = (players_index[pNum-1] + 1) > 3 ? 0 : players_index[pNum-1]+1;
		} else if ((players_axisY[pNum-1] < 0f) && playersBitmap [pNum-1] == 1 && canSwitchCharacterImage [pNum - 1]){
			players_panel[pNum-1].changePanelSprite(holdSprites[players_index[pNum-1]]);
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
