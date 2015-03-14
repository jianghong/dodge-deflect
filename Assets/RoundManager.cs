using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {

	public int roundCount = 3;
	void Awake() {
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void newRound() {
		roundCount -= 1;
		Application.LoadLevel ("scene4");
	}
}
