using UnityEngine;
using System.Collections;

public class LifeGameState : State {
	private PanelCenter pc;
	private static LifeGameState instance = null;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		pc = PanelCenter.Instance;
		pc.showGamePanel();
	}
	public override void execute(){
	}
	public override void exit(){
		Destroy (EntityManager.Instance.getEntityFromId (EntityType.LifeGamePanelEntity));
		EntityManager.Instance.removeEntity (EntityType.LifeGamePanelEntity);
	}
	public override bool onMessage(Message msg){
		if (msg.msg == MessageType.Msg_GameOver) {
			GameOverState.Instance.score = msg.score;
			pc.changeState(GameOverState.Instance);
			return true;
		}
		return false;
	}
	public static LifeGameState Instance{
		get{
			return instance;
		}
	}
}
