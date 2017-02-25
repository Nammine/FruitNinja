using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeControl : MonoBehaviour {
	public Text timeTxt;
	private float timeStart = 0;
	private float timeNow;
	public int time = 60;
	public bool isGameStart = false;
	private static TimeControl instance = null;
	void Awake(){
		instance = this;
	}
	void Update () {
		timeTxt.text = time + "";
		if (isGameStart) {
			timeNow = Time.time;
			if(timeNow - timeStart >= 1.0f){
				time--;
				timeStart = timeNow;
			}
			if(time == 0)
				isGameStart = false;
		}
	}
	public static TimeControl Instance{
		get{
			return instance;
		}
	}
}
