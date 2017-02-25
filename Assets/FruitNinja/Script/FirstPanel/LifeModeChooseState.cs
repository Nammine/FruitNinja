using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LifeModeChooseState : State {
	private FirstPanel firstPanel;
	private MessageType messageType = MessageType.Msg_Null;
	private static LifeModeChooseState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		firstPanel = FirstPanel.Instance;
		firstPanel.animators [1].enabled = true;
	}
	public override void execute(){
	}
	public override void exit(){
		switch (messageType) {
		case MessageType.Msg_Space :
		case MessageType.Msg_Push:
			List<Rigidbody2D> rigidbodies = new List<Rigidbody2D> ();
			for (int i = 0; i < firstPanel.fruits.Count; i++) {
				rigidbodies.Add (firstPanel.fruits [i].GetComponent<Rigidbody2D> ());
				rigidbodies [i].gravityScale = Singleton.gravityScale;
				firstPanel.buttons [i].enabled = false;
			}
			rigidbodies [1].AddForce (new Vector2 (0, Singleton.upForce));
			firstPanel.audioSource.loop = false;
			Singleton.playMusic (firstPanel.start, firstPanel.audioSource);
			break;
		case MessageType.Msg_Left:
		case MessageType.Msg_Right:
		case MessageType.Msg_SwipeRight:
			firstPanel.animators[1].enabled = false;
			break;
		}
	}
	public override bool onMessage(Message message){
		messageType = message.msg;

		switch (message.msg) {
		case MessageType.Msg_Space :
		case MessageType.Msg_Push:
			firstPanel.changeState(LifeStartState.Instance);
			return true;
		case MessageType.Msg_Left:
			firstPanel.changeState(TimeModeChooseState.Instance);
			return true;
		case MessageType.Msg_Right:
		case MessageType.Msg_SwipeRight:
			firstPanel.changeState(ExitModeChooseState.Instance);
			return true;
		case MessageType.Msg_Tab:
		case MessageType.Msg_SwipeLeft:
			firstPanel.changeState(LifeIntroState.Instance);
			return true;
		case MessageType.Msg_Esc:
			if(Screen.fullScreen){
				Singleton.WindowModeChange();
			}
			else{
				Singleton.FullScreenSet();
			}
			return true;
		default:
			return false;
		}
	}
	public static LifeModeChooseState Instance{
		get{
			return instance;
		}
	}
}
