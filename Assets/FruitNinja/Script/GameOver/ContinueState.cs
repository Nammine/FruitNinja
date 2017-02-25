using UnityEngine;
using System.Collections;

public class ContinueState : State {
	private GameObject currentPanel;
	private static ContinueState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		MessageDispatcher.Instance.dispatchMessage (2.0f, EntityType.CanvasEntity, MessageType.Msg_Revert, new Vector2(0,0), 0);
	}
	public override void execute(){
	}
	public override void exit(){
	}
	public override bool onMessage(Message message){
		return false;
	}
	public static ContinueState Instance{
		get{
			return instance;
		}
	}
}
