using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {
	public State currentState;
	public State previousState;

	public void changeState(State newState){
		GestureListener.ifDetectGesture = false;
		currentState.exit ();
		previousState = currentState;
		currentState = newState;
		currentState.enter ();
		GestureListener.ifDetectGesture = true;
	}
	
	public bool handleMessage(Message msg){
		if (currentState != null && currentState.onMessage (msg)) {
			return true;
		}
		return false;
	}
	
	public void revertToPreviousState(){
		changeState (previousState);
	}
}
