using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ReturnToMenuChooseState : State {
	private  GameOverListener gameOverListener;
	private MessageType messageType = MessageType.Msg_Null;
	private static ReturnToMenuChooseState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		gameOverListener = GameOverListener.Instance;
		gameOverListener.returnAni.enabled = true;
	}
	public override void execute(){
	}
	public override void exit(){
		switch (messageType) {
		case MessageType.Msg_Space :
		case MessageType.Msg_Push:
			Rigidbody2D continueRgd = gameOverListener.continueFruit.GetComponent<Rigidbody2D>();
			Rigidbody2D returnRgd = gameOverListener.returnMenuFruit.GetComponent<Rigidbody2D>();
			continueRgd.gravityScale = Singleton.gravityScale;
			returnRgd.gravityScale = Singleton.gravityScale;
			gameOverListener.continueBtn.enabled = false;
			gameOverListener.returnMenuBtn.enabled = false;
			returnRgd.AddForce (new Vector2 (0, Singleton.upForce));
			break;
		case MessageType.Msg_Left:
		case MessageType.Msg_Right:
		case MessageType.Msg_SwipeRight:
			gameOverListener.returnAni.enabled = false;
			break;
		}
		Debug.Log ("MenuExit");
	}
	public override bool onMessage(Message message){
		messageType = message.msg;
		switch (message.msg) {
		case MessageType.Msg_Space :
		case MessageType.Msg_Push:
			gameOverListener.changeState(ReturnToMenuState.Instance);
			return true;
		case MessageType.Msg_Left:
			gameOverListener.changeState(ContinueChooseState.Instance);
			return true;
		case MessageType.Msg_Right:
		case MessageType.Msg_SwipeRight:
			gameOverListener.changeState(ContinueChooseState.Instance);
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
	public static ReturnToMenuChooseState Instance{
		get{
			return instance;
		}
	}
}
