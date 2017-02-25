using UnityEngine;
using System.Collections;

public class ExitMode : State {
	private static ExitMode instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		Invoke ("exitGame", 2.0f);
	}
	private void exitGame(){
		Application.Quit ();
	}
	public static ExitMode Instance{
		get{
			return instance;
		}
	}
}
