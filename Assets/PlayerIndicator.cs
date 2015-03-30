using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {

	float bobSpeed;
	bool shouldBobUp = false;
	float bobDifference = 1f;
	float bobTime = 0.3f;
	Hashtable htUpParams = new Hashtable();
	Hashtable htDownParams = new Hashtable();

	// Use this for initialization
	void Start () {
		htUpParams.Add ("amount", new Vector3 (0f, 0f, bobDifference));
		htUpParams.Add ("time", bobTime);
		htUpParams.Add ("oncomplete", "unsetBobUp");
		htUpParams.Add ("space", Space.World);
		htDownParams.Add ("amount", new Vector3 (0f, 0f, -bobDifference));
		htDownParams.Add ("time", bobTime);
		htDownParams.Add ("oncomplete", "setBobUp");
		htDownParams.Add ("space", Space.World);
		iTween.MoveBy (gameObject, htUpParams);
	}
	
	// Update is called once per frame
	void Update () {
//		if (shouldBobUp) {
//			iTween.MoveBy (gameObject, htUpParams);
//		} else if (!shouldBobUp) {
//			iTween.MoveBy (gameObject, htDownParams);
//		}
	}

	void unsetBobUp() {
		iTween.MoveBy (gameObject, htDownParams);
		shouldBobUp = false;
	}

	void setBobUp() {
		iTween.MoveBy (gameObject, htUpParams);
		shouldBobUp = true;
	}
}
