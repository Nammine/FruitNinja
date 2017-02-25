using UnityEngine;
using System.Collections;

public class TimeIntroState : State {
	private FirstPanel firstPanel;
	private static TimeIntroState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		firstPanel = FirstPanel.Instance;
		firstPanel.tipPanels [0].SetActive (true);
	}
	public override void execute(){
	}
	public override void exit(){
		firstPanel.tipPanels [0].SetActive (false);
	}
	public override bool onMessage(Message message){
		if (message.msg == MessageType.Msg_SwipeRight || message.msg == MessageType.Msg_Esc) {
			firstPanel.changeState(TimeModeChooseState.Instance);
			return true;
		}
		return false;
	}
	public static TimeIntroState Instance{
		get{
			return instance;
		}
	}
}
