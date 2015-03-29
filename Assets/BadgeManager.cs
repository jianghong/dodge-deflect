using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BadgeManager : MonoBehaviour {
	
	Badge[] badges;
	int currBadge = 1;

	// Use this for initialization
	void Awake () {
		badges = GetComponentsInChildren<Badge> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setBadge(Texture t) {
		badges [currBadge].setBadge(t);
		badges [currBadge].showBadge ();
		currBadge += 1;
	}

	public void setExtraBadge(Texture t) {
		badges [0].setBadge (t);
		badges [0].showBadge ();
	}
}
