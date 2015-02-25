﻿using UnityEngine;
using System.Collections;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class MovePlayer : MonoBehaviour
{
	private const float MAX_TRG_SCL = 1.21f;
	public float BlockTime = 0f;				// Cooldown on blocking a ball.
	public GameObject Block;			// Placeholder to block the ball.
	public float speed = 6f;
	public float blockCD = 1f;

	public int playerNumber = 0;
	
	public Material matRed;
	public Material matGreen;
	public Material matBlue;
	public Material matYellow;
	
	public GameObject triggerLeftPrefab;
	public GameObject triggerRightPrefab;

	private static bool didQueryNumOfCtrlrs = false;

	Vector3 movement;                   // The vector to store the direction of the player's movement.
//	Rigidbody playerRigidbody;
	CharacterController controller;

	void Awake() {
//		playerRigidbody = GetComponent <Rigidbody> ();
		controller = GetComponent<CharacterController>();
	}
	// Start
	void Start ()
	{
		playerNumber = Mathf.Clamp(playerNumber, 1, 4);
		
		switch(playerNumber)
		{
		case 1: renderer.material = matRed; break;
		case 2: renderer.material = matGreen; break;
		case 3: renderer.material = matBlue; break;
		case 4: renderer.material = matYellow; break;
		}

		
		if(!didQueryNumOfCtrlrs)
		{
			didQueryNumOfCtrlrs = true;
			
			int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs();
			if(queriedNumberOfCtrlrs == 1)
			{
				Debug.Log("Only " + queriedNumberOfCtrlrs + " Xbox controller plugged in.");
			}
			else if (queriedNumberOfCtrlrs == 0)
			{
				Debug.Log("No Xbox controllers plugged in!");
			}
			else
			{
				Debug.Log(queriedNumberOfCtrlrs + " Xbox controllers plugged in.");
			}
			
			XCI.DEBUG_LogControllerNames();
		}
	}
	
	
	// Update
	void Update ()
	{
		float axisX = XCI.GetAxis(XboxAxis.LeftStickX, playerNumber);
		float axisY = XCI.GetAxis(XboxAxis.LeftStickY, playerNumber);

		// Left stick movement
		Move (axisX, axisY);
		
		// Right stick movement
		axisX = XCI.GetAxis(XboxAxis.RightStickX, playerNumber);
		axisY = XCI.GetAxis(XboxAxis.RightStickY, playerNumber);
		Rotate (axisX, axisY);

		Deflect ();
	}
	
	// Gizmo Drawing
	void OnDrawGizmos()
	{
		switch (playerNumber)
		{
		case 1:		Gizmos.color = Color.red; break;
		case 2:		Gizmos.color = Color.green; break;
		case 3:		Gizmos.color = Color.blue; break;
		case 4:		Gizmos.color = Color.yellow; break;
		default:	Gizmos.color = Color.white; break;
		}
		
		Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);
		
		Gizmos.DrawCube(transform.position, new Vector3(1.2f, 1.2f, 1.2f));
	}

	void Move (float h, float v)
	{
		Vector3 moveDirection = new Vector3(h, 0f, v);
		moveDirection *= speed;
//		// Set the movement vector based on the axis input.
//		movement.Set (h, 0f, v);
//		
//		// Normalise the movement vector and make it proportional to the speed per second.
//		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
//		playerRigidbody.AddForce (movement);

		controller.Move(moveDirection * Time.deltaTime);

	}

	void Rotate(float x, float y)
	{
		transform.LookAt(transform.position + new Vector3(x, 0.0f, y), Vector3.up);
	}
	

	void Deflect() {
		if (XCI.GetButtonDown(XboxButton.RightBumper, playerNumber)) {
			if (BlockTime == 0f) {
				BlockTime = Time.time;
				Block.transform.localScale += new Vector3 (1.5f, 0f, 1.5f);
			}
		}
		if (Time.time > BlockTime + .5) {
			Block.transform.localScale = new Vector3 (0.9f, 0.9f, 0.9f);
		}
		if (Time.time > BlockTime + blockCD) {
			BlockTime = 0f;
		}
	}
}
