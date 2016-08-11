using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LifeContent : MonoBehaviour {
	public Toggle life1;
	public Toggle life2;
	public Toggle life3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setLife(int lifeNum){
		if (lifeNum == 3) {
			life1.isOn = true;
			life2.isOn = true;
			life3.isOn = true;
		} else if (lifeNum == 2) {
			life1.isOn = false;
			life2.isOn = true;
			life3.isOn = true;
		} else if (lifeNum == 1) {
		
			life1.isOn = false;
			life2.isOn = false;
			life3.isOn = true;
		} else {

			life1.isOn = false;
			life2.isOn = false;
			life3.isOn = false;
		}
	}
}
