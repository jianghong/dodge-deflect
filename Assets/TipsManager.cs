using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class TipsManager : MonoBehaviour {
	string[] beginnerTips = {
		"Win a round to enter the glorious trophy round.",
		"Form Voids strategically by shooting a star into another",
		"Use the Right Stick to manually aim at your desired target",
		"Be careful of the gravity when close to voids",
		"Catch and hold (RT) stars to infuse their trail with your color. Stars that have a trail of your color don’t damage you",
		"Shoot a star into a Void to make it bigger and more threatening"
	};

	string[] advancedTips = {
		"Headbutt (LT) other players into voids for instant gratification",
		"Control the arena by infusing stars with your color so there are less things for you to avoid",
		"Stars that bounce off the metal walls will double in speed",
		"Time your attack right when the other player has just missed their catch/headbutt to catch them off guard"
	};


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTip(int round) {
		int tipIndex;
		if (round == 0) {
			GetComponent<Text> ().text = beginnerTips [0];
			beginnerTips = beginnerTips.Where ((val, idx) => idx != 0).ToArray ();
			Debug.Log (beginnerTips);
		} else if (round == 1) {
			tipIndex = Random.Range (1, beginnerTips.Length);
			GetComponent<Text> ().text = beginnerTips [tipIndex];
			beginnerTips = beginnerTips.Where ((val, idx) => idx != tipIndex).ToArray ();
			Debug.Log (beginnerTips);
		} else {
			tipIndex = Random.Range (0, advancedTips.Length);
			GetComponent<Text> ().text = advancedTips [tipIndex];
			beginnerTips = advancedTips.Where ((val, idx) => idx != tipIndex).ToArray ();
			Debug.Log (advancedTips);
		}
	}
}
