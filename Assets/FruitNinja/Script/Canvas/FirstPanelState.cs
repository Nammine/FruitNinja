using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class FirstPanelState : State  {
	private PanelCenter pc;
	private static FirstPanelState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		pc = PanelCenter.Instance;
		pc.showFirstPanel();
	}
	public override void execute(){
	}
	public override void exit(){
		Destroy (EntityManager.Instance.getEntityFromId (EntityType.FirstPanelEntity));
		EntityManager.Instance.removeEntity (EntityType.FirstPanelEntity);
		GestureListener.ifDetectGesture = false;
	}
	public override bool onMessage(Message msg){
		switch (msg.msg) {
		case MessageType.Msg_GotoTimeGame:
			pc.changeState((TimeGameState)TimeGameState.Instance);
			return true;
		case MessageType.Msg_GotoLifeGame:
			pc.changeState((LifeGameState)LifeGameState.Instance);
			return true;
		}
		return false;
	}
	public static FirstPanelState Instance{
		get{
			return instance;
		}
	}
}
