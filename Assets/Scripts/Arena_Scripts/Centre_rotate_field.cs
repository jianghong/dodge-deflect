using UnityEngine;
using System.Collections;

public class Centre_rotate_field : MonoBehaviour {
	public float rotateSpeed = 2f;

	// Update is called once per frame
	void Update () 
	{
	
		transform.Rotate (new Vector3 (0, rotateSpeed, 0) * Time.deltaTime);

	}
}
