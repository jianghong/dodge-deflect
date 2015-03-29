using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class RoundBoard : MonoBehaviour {
	
	public Sprite[] frameTextures;
	public Texture[] r1Textures;
	public Texture[] r2Textures;
	public Texture[] r3Textures;
	public GameObject plusOne;
	public Texture entryTexture;
	public Texture flawlessTexture;
	public Texture trophyRound;
	bool startCountDown = false;
	float timeStart;
	GameManager gm;
	bool oneTime = false;
	int timeToNextRound = 12;
	
	void Awake() {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (startCountDown) {
			transform.FindChild ("CountdownRow").GetComponentInChildren<Text>().text =  "PROCEEDING TO NEXT ROUND... " + (timeToNextRound - (Time.time - timeStart)).ToString ("F0");
			if ((Time.time - timeStart >= (timeToNextRound-1)) && !oneTime) {
				gm.newRound();
				oneTime = true;
			}
		}
	}
	
	public void setRoundVictor() {
		
		for (int i=0; i < gm.roundScores.Length; i++) {
			string roundObjectName;
			Texture[] roundTextures;
			switch (i) {
			case 0: roundObjectName = "Round1Point"; break;
			case 1: roundObjectName = "Round2Point"; break;
			case 2: roundObjectName = "Round3Point"; break;
			default: roundObjectName = "TrophyRoundPoint"; break;
			}
			switch (i) {
			case 0: roundTextures = r1Textures; break;
			case 1: roundTextures = r2Textures; break;
			case 2: roundTextures = r3Textures; break;
			default: roundTextures = r1Textures; break;
			}
			if (gm.roundScores[i] != 0) {
				transform.FindChild ("TextureRow").FindChild(roundObjectName).GetComponent<RawImage>().texture = roundTextures[gm.roundScores[i]-1];
			}
		}
		
		string winnerText;
		int latestRound = (3 - gm.roundCount);
		int latestRoundVictor = gm.roundScores [latestRound];
		switch(latestRoundVictor) {
		case 1: winnerText = "YELLOW WINS ROUND " + (latestRound + 1).ToString() + "!";break;
		case 2: winnerText = "GREEN WINS ROUND " + (latestRound + 1).ToString() + "!";break;
		case 3: winnerText = "BLUE WINS ROUND " + (latestRound + 1).ToString() + "!";break;
		case 4: winnerText = "PINK WINS ROUND " + (latestRound + 1).ToString() + "!";break;
		default: winnerText = "YELLOW WINS ROUND " + (latestRound + 1).ToString() + "!";break;
		}
		transform.FindChild("WinnerRow").GetComponentInChildren<Text>().text = winnerText;
		GetComponent<Image> ().sprite = frameTextures [latestRoundVictor - 1];
		
		string roundName;
		
		switch (latestRound) {
		case 0: roundName = "Round1Point"; break;
		case 1: roundName = "Round2Point"; break;
		case 2: roundName = "Round3Point"; break;
		default: roundName = "TrophyRoundPoint"; break;
		}
		
		Transform roundObject = transform.FindChild ("TextureRow").FindChild (roundName);
		GameObject plusOneUI = GameObject.Instantiate (plusOne, roundObject.position, Quaternion.identity) as GameObject;
		bool firstWin = true;
		for (int rs=0; rs<gm.roundScores.Length; rs++) {
			if ((gm.roundScores[rs] == latestRoundVictor) && (rs != latestRound)) {
				firstWin = false;
			}
		}
		if (firstWin) {
			plusOneUI.GetComponent<RawImage>().texture = entryTexture;
		}
		plusOneUI.transform.SetParent (roundObject.transform);
		
		if ((latestRound == 2) && (latestRoundVictor * 3 == gm.roundScores.Sum ())) {
			StartCoroutine(flawlessVictory());
		}
		
		timeStart = Time.time;
		startCountDown = true;
		
		transform.parent.Find ("TipsBox").GetComponentInChildren<TipsManager> ().setTip (latestRound);
	}
	
	IEnumerator flawlessVictory() {
		yield return new WaitForSeconds (1);
		Transform trophy = transform.FindChild ("TextureRow").FindChild ("TrophyRoundPoint");
		trophy.GetComponent<RawImage>().texture = trophyRound;
		GameObject flawless = GameObject.Instantiate (plusOne, trophy.position, Quaternion.identity) as GameObject;
		flawless.GetComponent<RawImage> ().texture = flawlessTexture;
		flawless.GetComponent<RawImage> ().SetNativeSize ();
		flawless.transform.SetParent (trophy);
	}
}
