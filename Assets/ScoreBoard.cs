	using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

	public GameObject[] PlayerScoreContainer;
	public Texture VoidLover;
	public Texture Headbutter;
	public Texture Avoider;
	public Texture StarHoarder;
	public Texture Flawless;
	public GameObject winnerText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setLifeSpans(float[] longestLifespans, float[] shortestLifespans) {
		for (int i=0; i<PlayerScoreContainer.Length; i++) {
			if (PlayerScoreContainer[i].activeSelf) {
				PlayerScoreContainer[i].transform.Find("Score Container").transform.Find ("Longest Life Span").transform.Find("LongestTime").GetComponent<Text>().text = longestLifespans[i].ToString("F1");
				PlayerScoreContainer[i].transform.Find("Score Container").transform.Find ("Shortest Life Span").transform.Find("ShortestTime").GetComponent<Text>().text = shortestLifespans[i].ToString("F1");
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
