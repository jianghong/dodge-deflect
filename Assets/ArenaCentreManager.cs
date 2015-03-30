using UnityEngine;
using System.Collections;

public class ArenaCentreManager : MonoBehaviour {

	public GameObject houseCentre;
	public GameObject doubleWallCentre;
	public GameObject islandCentre;
	GameManager gm;
	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		if (gm.roundCount == 3) {
			houseCentre.SetActive (true);
		} else if (gm.roundCount == 2) {
			doubleWallCentre.SetActive (true);
		} else if (gm.roundCount == 1) {
			houseCentre.SetActive (true);
		} else if (gm.isFinalRound) {
			islandCentre.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
