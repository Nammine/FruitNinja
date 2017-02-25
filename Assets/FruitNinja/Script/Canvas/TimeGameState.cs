using UnityEngine;
using System.Collections;

public class TimeGameState : State {
	private PanelCenter pc;
	private static TimeGameState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		pc = PanelCenter.Instance;
		pc.showGame2Panel();
	}
	public override void execute(){
	}
	public override void exit(){
		Destroy (EntityManager.Instance.getEntityFromId (EntityType.TimeGamePanelEntity));
		EntityManager.Instance.removeEntity (EntityType.TimeGamePanelEntity);
	}
	public override bool onMessage(Message msg){
		if (msg.msg == MessageType.Msg_GameOver) {
			GameOverState.Instance.score = msg.score;
			pc.changeState(GameOverState.Instance);
			return true;
		}
		return false;
	}
	public static TimeGameState Instance{
		get{
			return instance;
		}
	}
}
