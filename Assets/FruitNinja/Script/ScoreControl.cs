using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour {
	public int score = 0;
	//private Object moniter = new Object();
	public void changeScore(){
		if (true) {
			//lock (moniter) {
				score++;
//				print ("score is " + score);
			//}
		}

	}
}
