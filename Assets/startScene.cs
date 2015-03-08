using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class startScene : MonoBehaviour {

	string sceneToLoad = "scene4";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (XCI.GetButtonUp(XboxButton.Start, 1)) {
			Application.LoadLevel(sceneToLoad);
		}
		if (XCI.GetButtonUp(XboxButton.Start, 2)) {
			Application.LoadLevel(sceneToLoad);
		}	
		if (XCI.GetButtonUp(XboxButton.Start, 3)) {
			Application.LoadLevel(sceneToLoad);
		}
		if (XCI.GetButtonUp(XboxButton.Start, 4)) {
			Application.LoadLevel(sceneToLoad);
		}
	}
}
