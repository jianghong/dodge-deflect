using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundBoard_PlusOne : MonoBehaviour {
	Vector3 pos;
	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().enabled = false;
		StartCoroutine (DelayPlusOne ());
	}

	IEnumerator DelayPlusOne() {
		yield return new WaitForSeconds(0.6f);
		GetComponent<RawImage> ().enabled = true;
		iTween.MoveTo (gameObject, new Vector3(transform.position.x, transform.position.y + 85f, transform.position.z), 5f);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
