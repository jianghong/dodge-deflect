using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class startScene : MonoBehaviour {

	public string sceneToLoad = "scene4";
	public int fadeOutTime = 2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (XCI.GetButtonUp(XboxButton.Start, 1)) {
			AutoFade.LoadLevel(sceneToLoad, fadeOutTime, 1, Color.black);
		}
		if (XCI.GetButtonUp(XboxButton.Start, 2)) {
			AutoFade.LoadLevel(sceneToLoad, fadeOutTime, 1, Color.black);
		}	
		if (XCI.GetButtonUp(XboxButton.Start, 3)) {
			AutoFade.LoadLevel(sceneToLoad, fadeOutTime, 1, Color.black);
		}
		if (XCI.GetButtonUp(XboxButton.Start, 4)) {
			AutoFade.LoadLevel(sceneToLoad, fadeOutTime, 1, Color.black);
		}
	}
}
