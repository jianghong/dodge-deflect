using UnityEngine;
using System.Collections;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class MovePlayer : MonoBehaviour
{
	private const float MAX_TRG_SCL = 1.21f;
	public float BlockTime = 0f;				// Cooldown on blocking a ball.
	public GameObject Block;			// Placeholder to block the ball.
	public float speed = 6f;
	public float blockCD = 1f;
	public float blockerForce = 50f;

	public int playerNumber = 0;
	
	public Material matRed;
	public Material matGreen;
	public Material matBlue;
	public Material matYellow;
	
	public GameObject triggerLeftPrefab;
	public GameObject triggerRightPrefab;
	public float blockerTTL = 0.5f;
	private static bool didQueryNumOfCtrlrs = false;
	Vector3 shooterPos;
	public GameObject ballPrefab;
	Blocker blockerScript;
	shooterScript shooter;
	BallIndicatorControl ballControl;

	bool isHoldingProjectile = false;

	Vector3 movement;                   // The vector to store the direction of the player's movement.
//	Rigidbody playerRigidbody;
	CharacterController controller;
	bool controllerIsEnabled = true;

	void Awake() {
//		playerRigidbody = GetComponent <Rigidbody> ();
		controller = GetComponent<CharacterController>();
		blockerScript = Block.GetComponent<Blocker> ();
		ballControl = this.GetComponentInChildren<BallIndicatorControl> ();
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
		if (controllerIsEnabled) {
			float axisX = XCI.GetAxis (XboxAxis.LeftStickX, playerNumber);
			float axisY = XCI.GetAxis (XboxAxis.LeftStickY, playerNumber);

			// Left stick movement
			Move (axisX, axisY);

			// Right stick movement
			axisX = XCI.GetAxis (XboxAxis.RightStickX, playerNumber);
			axisY = XCI.GetAxis (XboxAxis.RightStickY, playerNumber);
			Rotate (axisX, axisY);

			Deflect ();
		}
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

		controller.Move(moveDirection * Time.deltaTime);

	}

	void Rotate(float x, float y)
	{
		transform.LookAt(transform.position + new Vector3(x, 0.0f, y), Vector3.up);
	}
	

	void Deflect() {
		if (XCI.GetButton(XboxButton.RightBumper, playerNumber) && !isHoldingProjectile) {
			if (BlockTime == 0f) {
				BlockTime = Time.time;
				blockerScript.activate();
				Block.transform.localScale += new Vector3 (1.5f, 0f, 1.5f);
			}
		}

		if (XCI.GetButtonUp (XboxButton.RightBumper, playerNumber) && isHoldingProjectile) {
			shooter = collider.gameObject.GetComponentInChildren<shooterScript>();
			shooterPos = shooter.getTransform().position;
			Vector3 newBallPos = new Vector3(shooterPos.x, 0.5f, shooterPos.z);
			GameObject createdBall = GameObject.Instantiate(ballPrefab, newBallPos, collider.transform.rotation) as GameObject;
			createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*blockerForce, ForceMode.Impulse);
			unsetIsHoldingProjectile();
		}
		if ((Time.time > BlockTime + blockerTTL) || isHoldingProjectile) {
			blockerScript.deactivate();
			Block.transform.localScale = new Vector3 (0.9f, 0.9f, 0.9f);
		}
		if (Time.time > BlockTime + blockCD) {
			BlockTime = 0f;
		}

	}

	public void setIsHoldingProjectile() {
		isHoldingProjectile = true;
		ballControl.show ();
	}

	public void unsetIsHoldingProjectile() {
		isHoldingProjectile = false;
		ballControl.hide ();
	}
	public void enableController() {
		controllerIsEnabled = true;
	}

	public void disableController() {
		controllerIsEnabled = false;
	}
}
