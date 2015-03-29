using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExtraBadge : MonoBehaviour {

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