using UnityEngine;
using System.Collections;

public class LifeIntroState : State {
	private FirstPanel firstPanel;
	private static LifeIntroState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		firstPanel = FirstPanel.Instance;
		firstPanel.tipPanels [1].SetActive (true);
	}
	public override void execute(){
	}
	public override void exit(){
		firstPanel.tipPanels [1].SetActive (false);
	}
	public override bool onMessage(Message message){
		if (message.msg == MessageType.Msg_SwipeRight || message.msg == MessageType.Msg_Esc) {
			firstPanel.revertToPreviousState();
			return true;
		}
		return false;
	}
	public static LifeIntroState Instance{
		get{
			return instance;
		}
	}
}
