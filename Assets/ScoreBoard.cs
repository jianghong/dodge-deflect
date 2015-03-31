using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;
using System.Linq;

public class ScoreBoard : MonoBehaviour {

	public GameObject[] PlayerScoreContainer;
	public Texture VoidLover;
	public Texture Headbutter;
	public Texture Avoider;
	public Texture StarHoarder;
	public Texture Flawless;
	public Texture Falafel;
	public GameObject winnerText;
	public GameObject promptText;
	GameManager gm;
	int[] playersReadyState = {0, 0, 0, 0};
	float countdownStart;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActiveAndEnabled) {
			for (int i=0; i<gm.playersBitmap.Length;i++) {
				if (gm.playersBitmap[i] == 1) {
					getReadyInput(i+1);
				}
			}
		}
		countdownToNextScene ();
	}

	void getReadyInput(int pNum) {
		if (playersReadyState [pNum - 1] == 0) {
			if (((XCI.GetAxis(XboxAxis.LeftTrigger, pNum) > 0) && (XCI.GetAxis(XboxAxis.LeftTrigger, pNum) != 0.5f))) {
				playersReadyState[pNum-1] = 1;
				if (playersReadyState.Sum() == gm.playersBitmap.Sum ()) {
					countdownStart = Time.time;
				}
			}
		}
	}

	void countdownToNextScene() {
		if (playersReadyState.Sum () == gm.playersBitmap.Sum ()) {
			float timer = (5f - (Time.time - countdownStart));
			promptText.GetComponent<Text>().text = "Returning to start screen ... " + timer.ToString("F0");
			StartCoroutine(goToStart());
		}
	}

	IEnumerator goToStart() {
		yield return new WaitForSeconds (4);
		Destroy (gm);
		AutoFade.LoadLevel("FINAL_startScene", 0.7f, 0.7f, Color.black);
	}

	public void setLifeSpans(float[] longestLifespans, int[] hitScores) {
		for (int i=0; i<PlayerScoreContainer.Length; i++) {
			if (PlayerScoreContainer[i].activeSelf) {
				PlayerScoreContainer[i].transform.Find("Score Container").transform.Find ("Longest Life Span").transform.Find("LongestTime").GetComponent<Text>().text = longestLifespans[i].ToString("F1");
				PlayerScoreContainer[i].transform.Find("Score Container").transform.Find ("Hits On Players").transform.Find("Hits").GetComponent<Text>().text = hitScores[i].ToString();
			}

		}
	}

	public void disablePlayerContainers(int[] playerBitmap) {
		for (int i=0; i<playerBitmap.Length; i++) {
			if (playerBitmap[i] == 0) {
				PlayerScoreContainer[i].SetActive(false);
			}
		}
	}

	public void assignBadge(int pNum, int badge) {
		Texture toAssign;

		switch (badge) {
			case 0: toAssign = Avoider;break;
			case 1: toAssign = VoidLover;break;
			case 2: toAssign = Headbutter;break;
			case 3: toAssign = StarHoarder;break;
			default: toAssign = Avoider; break;
		}
		PlayerScoreContainer [pNum].transform.Find ("Score Container").transform.Find ("Badges").GetComponent<BadgeManager> ().setBadge(toAssign);
	}

	public void setFlawless(int pNum) {
		PlayerScoreContainer [pNum-1].transform.Find ("Score Container").transform.Find ("Badges").GetComponent<BadgeManager> ().setExtraBadge(Flawless);
	}

	public void setFalafel(int pNum) {
		PlayerScoreContainer [pNum-1].transform.Find ("Score Container").transform.Find ("Badges").GetComponent<BadgeManager> ().setExtraBadge(Falafel);
	}


	public void displayWinner(int pNum) {
		string winner;
		switch (pNum) {
			case 1: winner = "Yellow" ;break;
			case 2: winner = "Green" ;break;
			case 3: winner = "Blue" ;break;
			case 4: winner = "Pink" ;break;
			default : winner = "Yellow" ;break;
		}
		winnerText.GetComponent<Text> ().text = winner + " wins!";
		PlayerScoreContainer [pNum - 1].transform.Find ("WinnerBorder").GetComponent<Image> ().enabled = true;
		PlayerScoreContainer [pNum - 1].transform.Find ("Trophy").GetComponent<Image> ().enabled = true;
	}
}
