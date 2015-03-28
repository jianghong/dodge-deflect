using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundBoard : MonoBehaviour {
	
	public Texture[] r1Textures;
	public Texture[] r2Textures;
	public Texture[] r3Textures;
	GameManager gm;
	void Awake() {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setRoundVictor() {
		for (int i=0; i < gm.roundScores.Length; i++) {
			string roundObjectName;
			Texture[] roundTextures;
			switch (i) {
				case 0:	roundObjectName = "Round1Point"; break;
				case 1: roundObjectName = "Round2Point"; break;
				case 2: roundObjectName = "Round3Point"; break;
				default: roundObjectName = "TrophyRoundPoint"; break;
			}
			switch (i) {
				case 0:	roundTextures = r1Textures; break;
				case 1: roundTextures = r2Textures; break;
				case 2: roundTextures = r3Textures; break;
				default: roundTextures = r1Textures; break;
			}
			if (gm.roundScores[i] != 0) {
				transform.FindChild ("TextureRow").FindChild(roundObjectName).GetComponent<RawImage>().texture = roundTextures[gm.roundScores[i]-1];
			}
		}
	}
}
