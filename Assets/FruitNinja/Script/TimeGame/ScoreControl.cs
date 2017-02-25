using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreControl : MonoBehaviour {

	public Text scoreTxt;
	public int score = 0;
	private static ScoreControl instance = null;
	void Awake(){
		instance = this;
	}
	void Update () {
		scoreTxt.text = score + "";
	}
	public static ScoreControl Instance{
		get{
			return instance;
		}
	}
}
