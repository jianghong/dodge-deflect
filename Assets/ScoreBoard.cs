using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

	Text scoreText;
	
	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text> ();
		scoreText.text = "";

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setScoreBoard(int p1D, int p1T, int p2D, int p2T, int p3D, int p3T, int p4D, int p4T) {
		scoreText.text = "Player 1 landed " + p1D + " deflected shots." + p1T + " Total shots\n" +
						"Player 2 landed " + p2D + " deflected shots." + p2T + " Total shots\n" +
						"Player 3 landed " + p3D + " deflected shots." + p3T + " Total shots\n" +
						"Player 4 landed " + p4D + " deflected shots." + p4T + " Total shots\n";
	}
}
