using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LifeStartState : State {
	private static LifeStartState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		MessageDispatcher.Instance.dispatchMessage (2.0f, EntityType.CanvasEntity, MessageType.Msg_GotoLifeGame, new Vector2(0,0), 0);
	}
	public override void execute(){
	}
	public override void exit(){
	}
	public override bool onMessage(Message message){
		return false;
	}
	public static LifeStartState Instance{
		get{
			return instance;
		}
	}
}
