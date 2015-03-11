using UnityEngine;
using System.Collections;

public class LionelDeflect : MonoBehaviour {

	Animator ac;
	Vector3 defaultScale;
	int hitFrame;
	public int defaultFrameCount = 35;


	// Use this for initialization
	void Start () {
		ac = GetComponent<Animator> ();
		defaultScale = this.transform.localScale;
		this.transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.frameCount - hitFrame) > defaultFrameCount) {
			this.transform.localScale = Vector3.zero;
		}
	}

	public void triggerIsHit() {
		this.transform.localScale = defaultScale;
		ac.SetTrigger ("isHit");
		hitFrame = Time.frameCount;
	}
}
