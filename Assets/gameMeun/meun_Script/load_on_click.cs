using UnityEngine;
using System.Collections;

public class load_on_click : MonoBehaviour {

	public void loadScene(string which_scene)
	{
		Application.LoadLevel(which_scene);
	}

}
