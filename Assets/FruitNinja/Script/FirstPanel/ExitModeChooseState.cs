using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExitModeChooseState : State {
	private FirstPanel firstPanel;
	private MessageType messageType = MessageType.Msg_Null;
	private static ExitModeChooseState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		firstPanel = FirstPanel.Instance;
		firstPanel.animators [2].enabled = true;
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
			rigidbodies [2].AddForce (new Vector2 (0, Singleton.upForce));
			firstPanel.audioSource.loop = false;
			Singleton.playMusic (firstPanel.start, firstPanel.audioSource);
			break;
		case MessageType.Msg_Left:
		case MessageType.Msg_Right:
		case MessageType.Msg_SwipeRight:
			firstPanel.animators[2].enabled = false;
			break;
		}
		Debug.Log ("MenuExit");
	}
	public override bool onMessage(Message message){
		messageType = message.msg;
		switch (message.msg) {
		case MessageType.Msg_Space :
		case MessageType.Msg_Push:
			firstPanel.changeState(ExitMode.Instance);
			return true;
		case MessageType.Msg_Left:
			firstPanel.changeState(LifeModeChooseState.Instance);
			return true;
		case MessageType.Msg_Right:
		case MessageType.Msg_SwipeRight:
			firstPanel.changeState(TimeModeChooseState.Instance);
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
	public static ExitModeChooseState Instance{
		get{
			return instance;
		}
	}
}
