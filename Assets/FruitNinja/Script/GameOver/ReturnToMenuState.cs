using UnityEngine;
using System.Collections;

public class ReturnToMenuState : State {
	private GameObject currentPanel;
	private static ReturnToMenuState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		MessageDispatcher.Instance.dispatchMessage (2.0f, EntityType.CanvasEntity, MessageType.Msg_GotoMenu, new Vector2(0,0), 0);
	}
	public override void execute(){
	}
	public override void exit(){
	}
	public override bool onMessage(Message message){
		return false;
	}
	public static ReturnToMenuState Instance{
		get{
			return instance;
		}
	}
}
