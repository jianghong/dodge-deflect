using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BadgeManager : MonoBehaviour {

	Badge[] badges;
	int currBadge = 0;

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
}
