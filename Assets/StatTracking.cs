using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;
using System.Linq;

public class StatTracking : MonoBehaviour {

	int[] voidLover = {0, 0, 0, 0};
	int[] headbutter = {0, 0, 0, 0};
	int[] starHoarder = {0, 0, 0, 0};
	public int[] avoider = {0, 0, 0, 0};
	GameManager gm;
	// Use this for initialization
	void Start () {
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int[] getStat(string stat) {
		int[] result = {0, 0, 0, 0};
		switch (stat) {
			case "VoidLover":   result = voidLover; break;
			case "Headbutter":  result = headbutter; break;
			case "StarHoarder": result = starHoarder; break;
			case "Avoider":		result = avoider; break;
			default: 	        result = result; break;
		}
		return result;
	}

	public void addToStat(string stat, int pNum, int value) {
		int i = pNum - 1;
		Debug.Log (stat);
		switch (stat) {
			case "VoidLover":   voidLover[i] += value; break;
			case "Headbutter":  headbutter[i] += value; break;
			case "StarHoarder":	starHoarder[i] += value; break;
			case "Avoider":		avoider[i] = value; break;
			default: 	        Debug.Log ("default"); break;
		}
	}
}

