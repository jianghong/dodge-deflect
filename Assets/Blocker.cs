using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {
	
	MovePlayer player;
	public bool isActive;
	GameManager gm;

	void Awake() {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		isActive = false;
		player = this.gameObject.GetComponentInParent<MovePlayer>();
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Ball") {
			if (isActive) {
				Destroy(collision.gameObject);
				player.setIsHoldingProjectile();
			}
		}
	}

	public void deactivate() {
		isActive = false;
	}

	public void activate() {
		isActive = true;
	}
}
