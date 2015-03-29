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
	public float lerpRate = 1f;
	Vector3 lerpTargetScale;
	GameObject[] playerObjs;
	GameObject[] starObjs;
	public AudioClip voidExplodes;
	public GameObject explosionParticle;
	AudioSource ac;
	float audioVolume;

	void Awake() {
		lerpTargetScale = this.transform.localScale;
	}
	void Start() {
		spawnTime = Time.time;
		ac = GetComponent<AudioSource> ();
		audioVolume = lerpTargetScale.magnitude / maxScale.magnitude;
		ac.Play ();
	}

	void Update() {
		audioVolume = lerpTargetScale.magnitude/maxScale.magnitude;
		ac.volume = audioVolume;
		playerObjs = GameObject.FindGameObjectsWithTag ("Player");
		starObjs = GameObject.FindGameObjectsWithTag ("Ball");
		if (Time.time > spawnTime + duration) {
			while (starCounter > 0) {
				GameObject createdBall = GameObject.Instantiate(ballPrefab, this.transform.position, Random.rotation) as GameObject;
				createdBall.GetComponent<NewBounce>().setSpawnedBy(GetInstanceID());
//				createdBall.GetComponent<NewBounce>().setIsHostile();
				createdBall.rigidbody.AddForce(createdBall.transform.forward.normalized*30f, ForceMode.Impulse);
				starCounter -= 1;
			}
			AudioSource.PlayClipAtPoint(voidExplodes, this.transform.position, audioVolume);
			Instantiate (explosionParticle, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	
		// lerp to target
		this.transform.localScale = Vector3.Lerp(this.transform.localScale, lerpTargetScale, 3f * Time.deltaTime * lerpRate);


		// sunction effect
		Suction ();

		//rotate
		transform.Rotate (new Vector3 (0f, 15f, 0f) * Time.deltaTime);

	}

	void Suction() {
		Vector3 suctionOrigin = this.transform.position;
		for (int i = 0; (i < playerObjs.Length); i++) {
			Vector3 playerOrigin = playerObjs[i].transform.position;
			Vector3 toSuctionOriginFromObject = suctionOrigin - playerOrigin;
			float radialSize = gameObject.GetComponent<SphereCollider>().radius * lerpTargetScale.y;
			float radialDistance = Vector3.Distance(playerOrigin, suctionOrigin) - radialSize;
			float suctionForce = (lerpTargetScale.magnitude)*0.3f;
			if (radialDistance < 20f){
				playerObjs[i].GetComponent<CharacterController>().Move(toSuctionOriginFromObject * suctionForce * Time.deltaTime);
			}
		}
		for (int i = 0; (i < starObjs.Length); i++) {
			Vector3 starOrigin = starObjs[i].transform.position;
			Vector3 toSuctionOriginFromObject = suctionOrigin - starOrigin;
			float radialSize = gameObject.GetComponent<SphereCollider>().radius * lerpTargetScale.y;
			float radialDistance = Vector3.Distance(starOrigin, suctionOrigin) - radialSize;
			float suctionForce = (lerpTargetScale.magnitude)*60f / radialDistance;
			if (radialDistance < 20f ){
				starObjs[i].rigidbody.AddForce(toSuctionOriginFromObject * suctionForce * Time.deltaTime);
			}
		}

	}
	public void scaleToStarCount() {
		for (int i = 0 ;(i < (starCounter-2)); i++) {
			newScale = lerpTargetScale * scaleMultiplier;
			if (newScale.magnitude < maxScale.magnitude) {
				lerpTargetScale = newScale;
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
				int starsToAdd = other.GetComponent<BlackHole>().starCounter;
				Destroy(other.gameObject);
				this.GetComponent<BlackHole>().starCounter += starsToAdd;
				this.GetComponent<BlackHole>().scaleToStarCount();
			}
		}
	}
}
