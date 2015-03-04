using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {

	public Vector3 defaultPosition = new Vector3(5f, -5f, 3f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void resetPosition() {
		transform.position = defaultPosition;
	}
}
