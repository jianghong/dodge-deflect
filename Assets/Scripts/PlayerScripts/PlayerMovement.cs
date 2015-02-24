using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;            // The speed that the player will move at.
	public float rotationSpeed;

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

	public GameObject Block;			// Placeholder to block the ball.
	float BlockTime = 0f;				// Cooldown on blocking a ball.

	void Awake ()
	{
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	
	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		float rotateX = Input.GetAxisRaw ("RotationX");
		float rotateY = Input.GetAxisRaw ("RotationY");
		
		// Move the player around the scene.
		Move (h, v);
		Rotate (rotateX, rotateY);

		if (Input.GetAxis ("Fire1") > 0) {
			if (BlockTime == 0f) {
				BlockTime = Time.time;
				Block.transform.localScale += new Vector3 (0f, 0f, .9f);
			}
		}
		if (Time.time > BlockTime + .5) {
			Block.transform.localScale = new Vector3 (0.9f, 0.9f, 0.1f);
		}
		if (Time.time > BlockTime + 2) {
			BlockTime = 0f;
		}
	}

	void Rotate(float x, float y) 
	{
		Debug.Log("X: " + x);
		Debug.Log("Y: " + y);

		transform.LookAt(transform.position + new Vector3(x, 0.0f, y), -Vector3.forward);

	}
	
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}
}