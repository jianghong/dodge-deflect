using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	AudioSource audio;
	float fadeTime = 3f;
	Hashtable ht = new Hashtable();
	Hashtable ht2 = new Hashtable ();
	AudioClip audioC;

	void Awake() {
		DontDestroyOnLoad (this);
		audio = GetComponent<AudioSource> ();
		ht.Add ("volume", 1f);
		ht.Add ("pitch", 1f);
		ht.Add ("time", 1f);
		ht2.Add ("volume", 0f);
		ht2.Add ("pitch", 1f);
		ht2.Add ("time", 1f);
		ht2.Add ("oncomplete", "fadeInClip");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setClip(AudioClip c, float fadeOut, float fadeIn) {
		ht2 ["time"] = fadeOut;
		audioC = c;
		ht ["time"] = fadeIn;
		iTween.AudioTo (gameObject, ht2);
	}

	void fadeInClip() {
		audio.clip = audioC;
		audio.Play ();
		iTween.AudioTo (gameObject, ht);
	}

	void playClip() {

	}

	public float getClipLength() {
		return audio.clip.length;
	}
}
