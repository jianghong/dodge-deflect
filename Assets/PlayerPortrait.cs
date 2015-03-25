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

	
	public void changePortrait(string s) {
		StartCoroutine (changeTo(s));
	}

	IEnumerator changeTo(string s) {
		Texture switchTo;
		switch (s) {
			case "hit": switchTo = hitPortrait; break;
			case "dead": switchTo = deadPortrait; break;
			default: switchTo = defaultPortrait; break;
		}
		GetComponent<RawImage> ().texture = switchTo;
		yield return new WaitForSeconds (0.5f);
		GetComponent<RawImage> ().texture = defaultPortrait;
	}
}
