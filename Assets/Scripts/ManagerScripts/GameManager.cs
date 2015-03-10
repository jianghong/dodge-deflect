using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public int minPlayers;
	public int numPlayers;
	public int[] playersBitmap = {0, 0, 0, 0};

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
}
