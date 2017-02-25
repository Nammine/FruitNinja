using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameoverState : State {
	private static GameoverState instance = null;
	public Image gameOverImg;
	public AudioClip over;
	private ScoreControl scoreControl;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		gameOverImg.gameObject.SetActive(true);
		gameObject.GetComponent<AudioSource> ().clip = over;
		gameObject.GetComponent<AudioSource> ().Play ();
		scoreControl = ScoreControl.Instance;
		Invoke ("destroyGamePanel", 4.0f);
	}
	public override void execute(){
	}
	public override void exit(){
	}
	public override bool onMessage(Message msg){
		return true;
	}

	public static GameoverState Instance{
		get{
			return instance;
		}
	}

	private void destroyGamePanel(){
		MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.CanvasEntity, MessageType.Msg_GameOver, new Vector2 (0, 0), scoreControl.score);
	}
}
