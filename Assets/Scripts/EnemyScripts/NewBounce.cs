using UnityEngine;
using System.Collections;

public class NewBounce : MonoBehaviour {
		
	public float initialForce = 20f;
	public float maxVelocity = 50f;
	public GameObject Ball;
	public GameObject voidIndicator;
//	public Material safeMat;
//	public Material hostileMat;
	public float TTL = 0.4f;
	public bool isHostile = true;
	public float hostileTime = 3f;
	public int shotByPNum = -1; // -1 means no player shot it

	public Material p1Trail, p2Trail, p3Trail, p4Trail;
	GameManager gm;
	Vector3 shooterPos;
	float spawnTime;
	int spawnedBy;
	bool deflectedStar = false;

	void Awake() {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		this.rigidbody.AddForce(transform.forward.normalized * initialForce);
	}

	void Update () {
//		if ((Time.time - spawnTime) > hostileTime) {
//			this.unsetIsHostile();		
//		}
	}

	public void setTrail(int pNum) {
		Debug.Log ("Trail p: " + pNum);
		switch(pNum)
		{
			case 1: gameObject.GetComponent<TrailRenderer>().material = p1Trail; break;
			case 2: gameObject.GetComponent<TrailRenderer>().material = p2Trail; break;
			case 3: gameObject.GetComponent<TrailRenderer>().material = p3Trail; break;
			case 4: gameObject.GetComponent<TrailRenderer>().material = p4Trail; break;
		}
	}

	bool canCollideWithSameSpawnedBy() {
		return ((Time.time - spawnTime) > TTL);
	}

	public void setSpawnedBy(int i) {
		spawnedBy = i;
	}

	public int getSpawnedBy() {
		return spawnedBy;
	}
//
//	public void setIsHostile () {
//		isHostile = true;
//		this.renderer.material = hostileMat;
//	}
//
//	public void unsetIsHostile () {
//		isHostile = false;
//		this.renderer.material = safeMat;
//	}

	public void setDeflectedStar(bool v) {
		deflectedStar = v;
	}

	public bool getDeflectedStar() {
		return deflectedStar;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.tag == "Ball") {
			if (collision.gameObject.GetComponent<NewBounce>().getSpawnedBy() == spawnedBy) {
				if (!canCollideWithSameSpawnedBy()) {
					return;
				}
			}
			Debug.Log ("VoidLover: " + shotByPNum);
			if (shotByPNum > -1) {
				gm.tracking.addToStat("VoidLover", shotByPNum, 1);
			}

			if (this.GetInstanceID() > collision.gameObject.GetInstanceID()) {
				Instantiate (voidIndicator, new Vector3 (this.transform.position.x, -0.6f, this.transform.position.z), Quaternion.identity);
			}
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag == "BlackHole") {
			Destroy(this.gameObject);
		}
	}
}
