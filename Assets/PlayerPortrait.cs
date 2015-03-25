using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPortrait : MonoBehaviour {

	public Texture defaultPortrait;
	public Texture hitPortrait;
	public Texture deadPortrait;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void changeToHitStarter() {
		StartCoroutine (changeToHit());
	}

	public void changeToDead() {
		GetComponent<RawImage> ().texture = deadPortrait;
	}

	public void changeToDefault() {
		GetComponent<RawImage> ().texture = defaultPortrait;
	}

	IEnumerator changeToHit() {
		GetComponent<RawImage> ().texture = hitPortrait;
		yield return new WaitForSeconds (0.75f);
		GetComponent<RawImage> ().texture = defaultPortrait;
	}
}
