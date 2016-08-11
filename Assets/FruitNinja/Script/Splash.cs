using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Splash : MonoBehaviour {
	private Image image;
	// Use this for initialization
	void Awake(){
		image = GetComponent<Image> ();
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setColor(int type){

		switch(type){
		case 1: image.color = Color.green;break;
		case 4: image.color = Color.yellow;break;
		case 7: image.color = Color.blue;break;
		case 10:image.color = Color.red ;break;
		case 13:image.color = Color.red;break;
		default:break;
		}
	
	}
}
