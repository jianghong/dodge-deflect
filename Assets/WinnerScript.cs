using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class WinnerScript : MonoBehaviour {

	Text winnerText;
	// Use this for initialization
	void Start () {
		winnerText = GetComponent<Text> ();
		winnerText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setWinnerText(int pNum) {
		winnerText.text = "PLAYER " + pNum + " IS THE WINNER!";
	}
}
