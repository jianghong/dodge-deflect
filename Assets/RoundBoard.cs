using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundBoard : MonoBehaviour {
	
	public Color[] colors;
	GameManager gm;
	void Awake() {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setRoundVictor() {
		for (int i=0; i < gm.roundScores.Length; i++) {
			string roundObjectName;
			switch (i) {
				case 0:	roundObjectName = "Round1Point"; break;
				case 1: roundObjectName = "Round2Point"; break;
				case 2: roundObjectName = "Round3Point"; break;
				default: roundObjectName = "VictoryRoundPoint"; break;
			}
			if (gm.roundScores[i] != 0) {
				transform.FindChild(roundObjectName).GetComponent<Image>().color = colors[gm.roundScores[i]-1];
			}
		}
	}
}
