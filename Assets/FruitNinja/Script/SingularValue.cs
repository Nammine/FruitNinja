using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SingularValue
{
	public static float minDistance = 100.0f;
	public static int playerNumber = 1;
	public static float x = 30.0f;
	public static float y = 30.0f;
	public static float radius = 15.0f;

	public static void playMusic(AudioClip clip, AudioSource source){
		source.clip = clip;
		source.Play ();
	}

}
