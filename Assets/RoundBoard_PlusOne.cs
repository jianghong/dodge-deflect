using UnityEngine;
using System.Collections;

public class RoundBoard_PlusOne : MonoBehaviour {

	Vector3 pos;
	// Use this for initialization
	void Start () {

		iTween.MoveTo (gameObject, new Vector3(transform.position.x, transform.position.y + 90f, transform.position.z), 3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
