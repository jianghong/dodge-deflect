using UnityEngine;
using System.Collections;

public class Centre_rotate_field : MonoBehaviour {


	// Update is called once per frame
	void Update () 
	{
	
		transform.Rotate (new Vector3 (0, 2, 0) * Time.deltaTime);

	}
}
