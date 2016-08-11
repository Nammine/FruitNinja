using UnityEngine;
using System.Collections;

public class OverMusic : MonoBehaviour {
	AudioSource sound;
	public AudioClip over;
	// Use this for initialization
	void Awake () {
		sound = gameObject.GetComponent<AudioSource> ();
		sound.PlayOneShot (over, 1.0F);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
