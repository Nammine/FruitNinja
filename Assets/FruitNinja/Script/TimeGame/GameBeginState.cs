using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameBeginState : State {
	private static GameBeginState instance = null;
	private Game2Rule gameRule;
	private ImageToUserMap imageToUserMap;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		imageToUserMap = GameObject.FindWithTag("Canvas").GetComponent<ImageToUserMap> ();
		gameRule = Game2Rule.Instance;
		if(Singleton.gameType == EntityType.TimeGamePanelEntity)
			MessageDispatcher.Instance.dispatchMessage (3.0f, EntityType.TimeGamePanelEntity, MessageType.Msg_GameStart, new Vector2(0,0), 0);
		else 
			MessageDispatcher.Instance.dispatchMessage (3.0f, EntityType.LifeGamePanelEntity, MessageType.Msg_GameStart, new Vector2(0,0), 0);
		imageToUserMap.kinectImg = gameRule.kinectImg;
		initIfFirstHandPos ();
		createMultiTrail ();
		createMultiCollider ();
		gameStartShow ();
	}
	public override void execute(){
	}
	public override void exit(){
	}
	public override bool onMessage(Message msg){
		if (msg.msg == MessageType.Msg_GameStart) {
			gameRule.changeState(GamingState.Instance);
			return true;
		}
		return false;
	}

	private void initIfFirstHandPos(){
		gameRule.ifFirstRightHandPos = new bool[Singleton.playerNumber];
		gameRule.ifFirstLeftHandPos = new bool[Singleton.playerNumber];
		for (int i = 0; i < Singleton.playerNumber; i++) {
			gameRule.ifFirstLeftHandPos[i] = true;
			gameRule.ifFirstRightHandPos[i] = true;
		}
	}

	private void createMultiTrail ()
	{
		gameRule.leftTrail = new GameObject[Singleton.playerNumber];
		gameRule.rightTrail = new GameObject[Singleton.playerNumber];
		for (int i = 0; i < Singleton.playerNumber; i++) {
			gameRule.leftTrail [i] = Instantiate (gameRule.trailClone) as GameObject;
			gameRule.leftTrail [i].transform.SetParent (transform);
			gameRule.leftTrail[i].transform.position = new Vector3(0,0,0);
			gameRule.leftTrail[i].transform.localScale = new Vector3(1,1,1);
			gameRule.rightTrail [i] = Instantiate (gameRule.trailClone) as GameObject;
			gameRule.rightTrail [i].transform.SetParent (transform);
			gameRule.rightTrail[i].transform.position = new Vector3(0,0,0);
			gameRule.rightTrail[i].transform.localScale = new Vector3(1,1,1);
		}
	}

	private void createMultiCollider(){
		gameRule.rightHandPosCollider = new GameObject[Singleton.playerNumber];
		gameRule.leftHandPosCollider = new GameObject[Singleton.playerNumber];
		gameRule.rightHandVelocity = new HandPosVelocity[Singleton.playerNumber];
		gameRule.leftHandVelocity = new HandPosVelocity[Singleton.playerNumber];
		gameRule.lastRightHandPos = new Vector2[Singleton.playerNumber];
		gameRule.lastLeftHandPos = new Vector2[Singleton.playerNumber];
		gameRule.rightHandRtf = new RectTransform[Singleton.playerNumber];
		gameRule.leftHandRtf = new RectTransform[Singleton.playerNumber];
		gameRule.rightHandCollider2D = new Collider2D[Singleton.playerNumber];
		gameRule.leftHandCollider2D = new Collider2D[Singleton.playerNumber];
		gameRule.rightHandImage = new Image[Singleton.playerNumber];
		gameRule.leftHandImage = new Image[Singleton.playerNumber];
		for (int i = 0; i < Singleton.playerNumber; i++) {
			gameRule.rightHandPosCollider[i] = Instantiate(gameRule.colliderColone) as GameObject;
			gameRule.rightHandCollider2D[i] = gameRule.rightHandPosCollider[i].GetComponent<Collider2D>();
			gameRule.rightHandImage[i] = gameRule.rightHandPosCollider[i].GetComponent<Image>();
			gameRule.rightHandRtf[i] = gameRule.rightHandPosCollider[i].transform as RectTransform;
			gameRule.rightHandPosCollider [i].transform.SetParent (transform);
			gameRule.rightHandRtf[i].anchoredPosition3D = new Vector3 (0, 0, 0);
			gameRule.rightHandRtf[i].localScale = new Vector3(1, 1, 1);
			gameRule.rightHandVelocity[i] = gameRule.rightHandPosCollider[i].GetComponent<HandPosVelocity>();
			gameRule.leftHandPosCollider[i] = Instantiate(gameRule.colliderColone) as GameObject;
			gameRule.leftHandCollider2D[i] = gameRule.leftHandPosCollider[i].GetComponent<Collider2D>();
			gameRule.leftHandImage[i] = gameRule.leftHandPosCollider[i].GetComponent<Image>();
			gameRule.leftHandRtf[i] = gameRule.leftHandPosCollider[i].transform as RectTransform;
			gameRule.leftHandPosCollider [i].transform.SetParent (transform);
			gameRule.leftHandRtf[i].anchoredPosition3D = new Vector3 (0, 0, 0);
			gameRule.leftHandRtf[i].localScale = new Vector3(1, 1, 1);
			gameRule.leftHandVelocity[i] = gameRule.leftHandPosCollider[i].GetComponent<HandPosVelocity>();
		}
	}


	private void gameStartShow(){
		Animation ani = gameRule.gameStart.GetComponent<Animation> ();
		ani.Play ();
		Invoke ("gameStartNextStage", 1.0f);
	}

	private void gameStartNextStage(){
		gameRule.gameStart.enabled = false;
		gameRule.gameStart.text = "START";
		gameRule.gameStart.enabled = true;
		Animation gameStartAnimation = gameRule.gameStart.GetComponent<Animation> ();
		gameStartAnimation ["GameStart"].time = 0;
		gameStartAnimation.Sample ();
		Invoke ("destroyGameStart", 1.0f);
	}
	
	private void destroyGameStart(){
		Destroy (gameRule.gameStart.gameObject);
	}

	public static GameBeginState Instance{
		get{
			return instance;
		}
	}
}
