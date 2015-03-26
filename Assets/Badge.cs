using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Badge : MonoBehaviour {

	RawImage badge;

	// Use this for initialization
	void Awake () {
		badge = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setBadge(Texture t) {
		badge.texture = t;
	}

	public void hideBadge() {
		badge.enabled = false;
	}

	public void showBadge() {
		badge.enabled = true;
	}
}
