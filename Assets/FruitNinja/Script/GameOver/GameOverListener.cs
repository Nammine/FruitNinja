using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverListener : MonoBehaviour
{
	private StateMachine stateMachine;
	private static GameOverListener instance = null;
	public RawImage kinectImg;
	public Animator continueAni;
	public Animator returnAni;
	public Text continueBtn;
	public Text returnMenuBtn;
	public Image continueFruit;
	public Image returnMenuFruit;
	public Text scoreTxt;

	void Start ()
	{
		instance = this;
		stateMachine = gameObject.GetComponent<StateMachine> ();
		stateMachine.currentState = GameOverMenuState.Instance;
		stateMachine.currentState.enter ();
		stateMachine.previousState = null;
	}

	public void changeState(State newState){
		stateMachine.changeState (newState);
	}
	
	public void revertToPreviousState(){
		stateMachine.revertToPreviousState ();
	}


	public static GameOverListener Instance{
		get{
			return instance;
		}
	}

}
