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
	public float deflectForce = 100f;
	public float deflectBlockSize = 7f;
	public float holdBlockSize = 8f;
	public float initialBlockerSize = 0.9f;
	public AudioClip holdShootAudioClip;
	public AudioClip[] deflectAudioClips;
	int deflectAudioClipIndex = 0;
	AudioSource audioSource;

	public int playerNumber = 0;
	
	public Material matRed;
	public Material matGreen;
	public Material matBlue;
	public Material matYellow;

	public Material trailYellow;
	public Material trailGreen;
	public Material trailBlue;
	public Material trailPink;

	public GameObject triggerLeftPrefab;
	public GameObject triggerRightPrefab;
	public float blockerTTL = 0.5f;
	public float deflectTTL = 0.2f;
	float TTLtype;
	private static bool didQueryNumOfCtrlrs = false;
	Vector3 shooterPos;
	public GameObject ballPrefab;
	Blocker blockerScript;
	shooterScript shooter;
	BallIndicatorControl ballControl;

	public bool isHoldingProjectile = false;

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	CharacterController controller;
	bool controllerIsEnabled = true;
	float controllerDeadZoneThreshold = 0.25f;
	bool deflectPressed = false;
	Animator animator;
	bool deflectFire;

	public float defaultColliderRadius = 2.21f;
	public float holdColliderRadius = 3.37f;
	Collider headbuttBox;
	public GameManager.ControlType controlType;

	void Awake() {
		controller = GetComponent<CharacterController>();
		blockerScript = Block.GetComponent<Blocker> ();
		ballControl = this.GetComponentInChildren<BallIndicatorControl> ();
	}
	// Start
	void Start ()
	{
		playerNumber = Mathf.Clamp(playerNumber, 1, 4);
		headbuttBox = this.transform.Find ("HeadbuttBox").collider;

		switch(playerNumber)
		{
			case 1: gameObject.GetComponentInChildren<SkinnedMeshRenderer> ().material = matRed; break;
			case 2: gameObject.GetComponentInChildren<SkinnedMeshRenderer> ().material = matGreen; break;
			case 3: gameObject.GetComponentInChildren<SkinnedMeshRenderer> ().material = matBlue; break;
			case 4: gameObject.GetComponentInChildren<SkinnedMeshRenderer> ().material = matYellow; break;
		}
		switch(playerNumber)
		{
			case 1: gameObject.GetComponent<TrailRenderer>().material = trailYellow; break;
			case 2: gameObject.GetComponent<TrailRenderer>().material = trailGreen; break;
			case 3: gameObject.GetComponent<TrailRenderer>().material = trailBlue; break;
			case 4: gameObject.GetComponent<TrailRenderer>().material = trailPink; break;
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
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource> ();
	}
	
	
	// Update
	void Update ()
	{
		if (controllerIsEnabled) {
			float axisX = XCI.GetAxis (XboxAxis.LeftStickX, playerNumber);
			float axisY = XCI.GetAxis (XboxAxis.LeftStickY, playerNumber);

			// Left stick movement
			Move (axisX, axisY);

			if (controlType == GameManager.ControlType.Auto) {
				AutoRotate(axisX, axisY);
			} else if (controlType == GameManager.ControlType.Manual) {
				// Right stick movement
				axisX = XCI.GetAxis (XboxAxis.RightStickX, playerNumber);
				axisY = XCI.GetAxis (XboxAxis.RightStickY, playerNumber);
				ManualRotate(axisX, axisY);
			}

			Hold ();
			Deflect();
			resetBlocker();
			// lerp blocker
			if (!blockerScript.isActive) {
				blockerScript.transform.localScale = Vector3.Lerp(blockerScript.transform.localScale, new Vector3(initialBlockerSize, initialBlockerSize, initialBlockerSize), 2f * Time.deltaTime);
			}
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

	void AutoRotate(float h, float v) {
		transform.LookAt (transform.position + new Vector3 (h, 0.0f, v), Vector3.up);
	}

	void Move (float h, float v)
	{
		if ((h != 0) || (v != 0)) {
			animator.SetBool("isMoving", true);
		} else {
			animator.SetBool ("isMoving", false);
		}

		Vector3 moveDirection = new Vector3(h, 0.0f, v);
		this.transform.position = new Vector3 (transform.position.x, 0f, transform.position.z);
		moveDirection *= speed;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void ManualRotate(float x, float y)
	{	
		Vector2 stickInput = new Vector2 (x, y);
		if (stickInput.magnitude < controllerDeadZoneThreshold) {
			x = 0f;
			y = 0f;
		}
		transform.LookAt (transform.position + new Vector3 (x, 0.0f, y), Vector3.up);
	}

	void resetBlocker() {
		TTLtype = deflectPressed ? deflectTTL : blockerTTL;
		bool enoughTimePassed = Time.time > BlockTime + TTLtype;
		if (enoughTimePassed && !isHoldingProjectile) {
			animator.SetBool ("isHolding", false);
			controller.radius = defaultColliderRadius;
		}
		if (enoughTimePassed || isHoldingProjectile) {
			blockerScript.deactivate();
			headbuttBox.enabled = false;
			deflectPressed = false;
		}
		if (Time.time > BlockTime + blockCD) {
			BlockTime = 0f;
		}
	}

	void createBall(bool isDeflect) {
		shooter = collider.gameObject.GetComponentInChildren<shooterScript>();
		shooterPos = shooter.getTransform().position;
		Vector3 newBallPos = new Vector3(shooterPos.x, 0.5f, shooterPos.z);
		GameObject createdBall = GameObject.Instantiate(ballPrefab, newBallPos, collider.transform.rotation) as GameObject;
		NewBounce createdBallScript = createdBall.GetComponent<NewBounce> ();
//		createdBallScript.setIsHostile();
		createdBallScript.setDeflectedStar(isDeflect);
		createdBallScript.shotByPNum = playerNumber;
		createdBall.GetComponent<TrailRenderer> ().material = gameObject.GetComponent<TrailRenderer> ().material;
//		createdBallScript.setTrail (playerNumber);
		if (isDeflect) {
			createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*deflectForce, ForceMode.Impulse);		
		} else {
			createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*blockerForce, ForceMode.Impulse);		
		}

	}

	void Deflect() {
		deflectFire = (XCI.GetAxis (XboxAxis.LeftTrigger, playerNumber) > 0 && XCI.GetAxis (XboxAxis.LeftTrigger, playerNumber) != 0.5f);

		// headbutt
		if (deflectFire) {
			headbuttBox.enabled = true;
		}

		if (deflectFire && !isHoldingProjectile) {
			if (BlockTime == 0f) {
				BlockTime = Time.time;
				blockerScript.activate();
				animator.SetTrigger("deflectPressed");
				Block.transform.localScale = new Vector3 (deflectBlockSize, deflectBlockSize,deflectBlockSize);
				deflectPressed = true;
			}
		}

		if (isHoldingProjectile && deflectPressed) {
			createBall (true);
			deflectPressed = false;
			unsetIsHoldingProjectile();
			playClip (deflectAudioClips[deflectAudioClipIndex]);
			deflectAudioClipIndex = (deflectAudioClipIndex + 1) > 2 ? 0 : deflectAudioClipIndex + 1; 
			blockerScript.deactivate();
		}
	}

	void Hold() {
		bool fire = (XCI.GetAxis (XboxAxis.RightTrigger, playerNumber) > 0 && XCI.GetAxis (XboxAxis.RightTrigger, playerNumber) != 0.5f);
		bool release = XCI.GetAxis (XboxAxis.RightTrigger, playerNumber) == 0;
		if (fire && !isHoldingProjectile) {
			if (BlockTime == 0f) {
				animator.SetBool ("isHolding", true);
				controller.radius = holdColliderRadius;
				BlockTime = Time.time;
				blockerScript.activate();
				Block.transform.localScale = new Vector3 (holdBlockSize, holdBlockSize, holdBlockSize);
			}
		}
		if (release && !deflectFire && isHoldingProjectile && !blockerScript.isActive) {
			createBall (false);
			playClip(holdShootAudioClip);
			unsetIsHoldingProjectile();
		}
	}

	void playClip(AudioClip ac) {
		audioSource.clip = ac;
		audioSource.Play ();
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
