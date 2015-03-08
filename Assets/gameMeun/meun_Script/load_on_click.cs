using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class load_on_click : MonoBehaviour {

	void Update() 
	{
		if (XCI.GetButtonUp(XboxButton.Start, 1)) {
			Debug.Log("Start pressed");
			loadScene("scene4");
		}
	}
	public void loadScene(string which_scene)
	{
		Debug.Log ("Clicked");
		Application.LoadLevel(which_scene);
	}

}
