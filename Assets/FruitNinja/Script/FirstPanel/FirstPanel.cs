using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FirstPanel : MonoBehaviour
{
	public List<Image> buttons;
	public List<Image> fruits;
	public List<Animator> animators;
	public AudioSource audioSource;
	public AudioClip menu;
	public AudioClip start;
	public RawImage kinectImg;
	public List<GameObject> tipPanels;
	private StateMachine stateMachine;
	private static FirstPanel instance = null;

	void Start(){
		instance = this;
		stateMachine = gameObject.GetComponent<StateMachine> ();
		stateMachine.currentState = MenuState.Instance;
		stateMachine.currentState.enter ();
		stateMachine.previousState = null;
	}

	public void changeState(State newState){

		stateMachine.changeState (newState);
	}
	
	public void revertToPreviousState(){
		stateMachine.revertToPreviousState ();
	}


	public static FirstPanel Instance{
		get{
			return instance;
		}
	}
}
