using UnityEngine;
using System.Collections;

public class load_on_click : MonoBehaviour {

	public void loadScene(string which_scene)
	{
//		Debug.Log ("Clicked");
		Application.LoadLevel(which_scene);
	}

}
