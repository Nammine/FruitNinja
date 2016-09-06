using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeNumControl : MonoBehaviour {
	public int lifeNum = 3;
	//private Object moniter = new Object();
	public void changeLifeNum(){
		if (lifeNum > 0) {
			//lock (moniter) {
				lifeNum--;
//				print ("lifeNum is " + lifeNum);
			//}
		}
	}
}
