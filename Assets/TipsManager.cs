using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class TipsManager : MonoBehaviour {
	string[] beginnerTips = {
		"Win a round to enter the take-all trophy round!",
		"Use the right stick for extra precise aiming!",
		"Avoid the voids’ gravitational pull!",
		"Catch (RT) stars to infuse them with your color, making them harmless to you!",
		"Shoot stars into voids to make them bigger and badder."
	};

	string[] advancedTips = {
		"Headbutt (LT) players into voids for instant gratification.",
		"Shoot stars into each other to form voids strategically!",
		"Control the arena by filling it with stars of your color. Less for you to avoid!",
		"Bounce stars off the metal to double their speed!",
		"Catch your opponents off guard when they mistime a catch or headbutt."
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
