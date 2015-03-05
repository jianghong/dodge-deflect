using UnityEngine;
using System.Collections;

public class BallIndicatorControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3 (0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void show() {
		this.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
	}

	public void hide() {
		this.transform.localScale = new Vector3 (0f, 0f, 0f);
	}
}
