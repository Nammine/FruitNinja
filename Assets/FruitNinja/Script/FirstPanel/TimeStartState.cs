using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimeStartState : State {
	private GameObject currentPanel;
	private static TimeStartState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		MessageDispatcher.Instance.dispatchMessage (2.0f, EntityType.CanvasEntity, MessageType.Msg_GotoTimeGame, new Vector2(0,0), 0);
	}
	public override void execute(){
	}
	public override void exit(){
	}
	public override bool onMessage(Message message){
		return false;
	}
	public static TimeStartState Instance{
		get{
			return instance;
		}
	}
}
