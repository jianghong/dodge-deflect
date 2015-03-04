using UnityEngine;
using System.Collections;

public class SpotlightControl : MonoBehaviour {

	Light l;
	// Use this for initialization
	void Start () {
		l = this.gameObject.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void lowerIntesity(int health) {
		float decrement = (l.spotAngle / health);
		l.spotAngle = decrement;
	}
}
