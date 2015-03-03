using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public GameObject ballPrefab;
	public GameObject bhPrefab;
	public int starCounter = 2;
	public float spawnTime = 0f;
	public float duration = 5f;
	public Vector3 maxScale = new Vector3(20f, 20f, 20f);
	public float scaleMultiplier = 1.25f;
	BallSpawnManager ballSpawnManager;
	Vector3 newScale;
	float lerpFrac = 0f;
	public float lerpRate = 0.1f;
	Vector3 lerpTargetScale;
	GameObject[] playerObjs;


	void Awake() {
		Debug.Log ("void awake");
		lerpTargetScale = this.transform.localScale;
	}
	void Start() {
		Debug.Log ("void start");
		spawnTime = Time.time;
	}

	void Update() {
		playerObjs = GameObject.FindGameObjectsWithTag ("Player");
		if (Time.time > spawnTime + duration) {
			while (starCounter > 0) {
				GameObject createdBall = GameObject.Instantiate(ballPrefab, this.transform.position, Random.rotation) as GameObject;
				createdBall.GetComponent<NewBounce>().setSpawnedBy(GetInstanceID());
				createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*30f, ForceMode.Impulse);
				starCounter -= 1;
			}
			Destroy (this.gameObject);
		}

		// lerp to target
		lerpFrac += Time.deltaTime * lerpRate;
		this.transform.localScale = Vector3.Lerp(this.transform.localScale, lerpTargetScale, lerpFrac);

		// sunction effect
		Suction ();
	}

	void Suction() {
		Vector3 suctionOrigin = this.transform.position;
		for (int i = 0; (i < playerObjs.Length); i++) {
			Vector3 playerOrigin = playerObjs[i].transform.position;
			Vector3 toSuctionOriginFromObject = suctionOrigin - playerOrigin;
			float suctionForce = lerpTargetScale.magnitude*0.02f;
			float radialSize = gameObject.GetComponent<SphereCollider>().radius * lerpTargetScale.y;
			float radialDistance = Vector3.Distance(playerOrigin, suctionOrigin) - radialSize;
			Debug.Log(radialDistance);
			if (radialDistance < 15f){
				playerObjs[i].GetComponent<CharacterController>().Move(toSuctionOriginFromObject * suctionForce * Time.deltaTime);
			}
		}

	}
	public void scaleToStarCount() {
		Debug.Log ("scale to starcount");
		for (int i = 0 ;(i < (starCounter-2)); i++) {
			newScale = lerpTargetScale * scaleMultiplier;
			Debug.Log (newScale);
			if (newScale.magnitude < maxScale.magnitude) {
				lerpTargetScale = newScale;
				Debug.Log (lerpTargetScale);
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.collider.gameObject.tag == "Ball") {
			starCounter += 1;
			newScale = this.transform.localScale * scaleMultiplier;
			if (newScale.magnitude < maxScale.magnitude) {		
				lerpTargetScale = newScale;
			}
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "BlackHole") {
			if (this.GetInstanceID() > other.GetInstanceID()) {
				Debug.Log ("Creating super BH");
				GameObject createdVoid = GameObject.Instantiate (bhPrefab, new Vector3 (this.transform.position.x, 0.5f, this.transform.position.z), Quaternion.identity) as GameObject;
				createdVoid.GetComponent<BlackHole>().starCounter += starCounter;
				createdVoid.GetComponent<BlackHole>().starCounter += other.GetComponent<BlackHole>().starCounter;
				createdVoid.GetComponent<BlackHole>().scaleToStarCount();
			}
			Destroy(this.gameObject);
		}
	}
}
