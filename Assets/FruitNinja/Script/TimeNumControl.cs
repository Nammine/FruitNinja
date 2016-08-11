using UnityEngine;
using System.Collections;

public class TimeNumControl : MonoBehaviour {
	public int timeLeast = 60;
	private int timeMax;
	private int chazhi;
	public bool ifCut = false;
	// Use this for initialization
	void Start () {
		timeMax = (int)Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		chazhi = (int)Time.fixedTime - timeMax;
		if (ifCut) {
			timeLeast = timeLeast - chazhi - 3;
			timeMax = (int)Time.time;
			ifCut = false;
		} else {
			timeLeast = timeLeast - chazhi;
			timeMax = (int)Time.time;
		}
	}
}
