using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

	public GameObject[] PlayerScoreContainer;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setLifeSpans(float[] longestLifespans, float[] shortestLifespans) {
		for (int i=0; i<PlayerScoreContainer.Length; i++) {
			if (PlayerScoreContainer[i].activeSelf) {
				PlayerScoreContainer[i].transform.Find("Score Container").transform.Find ("Longest Life Span").transform.Find("LongestTime").GetComponent<Text>().text = longestLifespans[i].ToString("F2") + "s";
				PlayerScoreContainer[i].transform.Find("Score Container").transform.Find ("Shortest Life Span").transform.Find("ShortestTime").GetComponent<Text>().text = shortestLifespans[i].ToString("F2") + "s";
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
}
