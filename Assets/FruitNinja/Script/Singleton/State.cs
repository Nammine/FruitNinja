using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {
	public virtual void enter(){
		Debug.Log ("enter");
	}
	public virtual void execute(){
		Debug.Log ("execute");
	}
	public virtual void exit(){
		Debug.Log ("exit");
	}
	public virtual bool onMessage(Message msg){
		Debug.Log ("message");
		return true;
	}
}
