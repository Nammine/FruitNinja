using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GamingState : State {
	private static GamingState instance = null;
	private Game2Rule gameRule;
	private LifeContent lifeControl;
	private TimeControl timeControl;
	public GameObject[] fruitClone;
	public GameObject bombClone;
//	private int forceHalfY = 3000;
	private const int minX = -288;
	private const int maxX = 288;
	private const int fruitY = -275;
	private ScoreControl scoreControl;
	public GameObject bombBlowReflect;
	void Awake(){
		instance = this;
	}
	public override void enter(){
		gameRule = Game2Rule.Instance;
		if (Singleton.gameType == EntityType.TimeGamePanelEntity) {
			timeControl = TimeControl.Instance;
			timeControl.isGameStart = true;
			MessageDispatcher.Instance.dispatchMessage (60.0f, EntityType.TimeGamePanelEntity, MessageType.Msg_GameOver, new Vector2(0,0), 0);
			MessageDispatcher.Instance.dispatchMessage (56.0f, EntityType.TimeGamePanelEntity, MessageType.Msg_AlmostOver, new Vector2(0,0), 0);
		} else {
			lifeControl = LifeContent.Instance;
		}
		scoreControl = ScoreControl.Instance;
		InvokeRepeating ("generateFruits", 0.0f, 3.0f);
	}
	public override void execute(){
	}
	public override void exit(){
		CancelInvoke();
		if (KinectManager.Instance.enabled == false)
			KinectManager.Instance.enabled = true;
	}
	public override bool onMessage(Message msg){
		switch (msg.msg) {
		case MessageType.Msg_AlmostOver:
			CancelInvoke("generateFruits");
			createLotsOfFruits();
			return true;
		case MessageType.Msg_GameOver:
			destroyTag();
			gameRule.changeState(GameoverState.Instance);
			return true;
		case MessageType.Msg_Scored:
			scoreControl.score++;
			return true;
		case MessageType.Msg_CutBomb:
			if(Singleton.gameType == EntityType.TimeGamePanelEntity){
				if (scoreControl.score >= 3)
					scoreControl.score -= 3;
				else
					scoreControl.score = 0;
			}
			else {
				if(lifeControl.lifeNum > 0)
					lifeControl.lifeNum--;
			}
			cutBomb(msg.bombPosition);
			return true;
		default:
			return false;
		}
	}

	private void generateFruits(){
		int mode = Random.Range (0, 1);
		if (mode == 0) {
			createMulFruit();
		} else {
			constantCreateFruit();
		}
	}

	private void createMulFruit ()
	{
		int num = Random.Range (3, 6);
		for (int i = 0; i<num; i++) {
			createFruit ();
		}
		createMulBomb ();
	}
	
	private void constantCreateFruit(){
		int num = Random.Range (3, 6);
		float time = System.Convert.ToSingle (3) / num;
		InvokeRepeating ("createFruit", 0.0f, time);
		Invoke ("stopFruitRepeating", 2.5f);
		int ifCreateBomb = Random.Range (0, 9);
		if (ifCreateBomb < 4) {
			int bombNum = Random.Range(1,3);
			float bombTime = System.Convert.ToSingle (1.5f) / bombNum;
			InvokeRepeating("createBomb", 0.0f, bombTime);
			Invoke ("stopBombRepeating", 1.5f);
		}
	}
	
	private void createFruit ()
	{
		GameObject currentFruit;
		int forceX;
		int forceY;
		Vector2 force;
		int[] types = {
			Contant.Type_Apple ,
			Contant.Type_Banana ,
			Contant.Type_Basaha,
			Contant.Type_Peach ,
			Contant.Type_Sandia
		};
		int type = Random.Range (1, 5);
		currentFruit = Instantiate (fruitClone [type]) as GameObject;
		currentFruit.GetComponent<DetectIfCut> ().fruitType = types [type];
		currentFruit.transform.SetParent (transform);
		RectTransform fruitRt = currentFruit.transform as RectTransform;
		int fruitX = Random.Range (minX, maxX); 
		fruitRt.anchoredPosition3D = new Vector3 (fruitX, fruitY, 0);
		fruitRt.localScale = new Vector3 (1, 1, 1);
		Rigidbody2D rigid2d = currentFruit.GetComponent<Rigidbody2D> ();
		forceX = Random.Range(3, 10) * 100;
		forceY = Random.Range (6, 8) * 1000;
		if (fruitX > 0) {
			force = new Vector2(-forceX, forceY);
		} else {
			force = new Vector2(forceX, forceY);
		}
		rigid2d.AddForce (force);
	}

	private void createBomb (){
		GameObject currentBomb;
		int forceX;
		int forceY;
		Vector2 force;
		currentBomb = Instantiate (bombClone) as GameObject;
		currentBomb.GetComponent<DetectIfCut> ().fruitType = Contant.Type_Boom;
		currentBomb.transform.SetParent (transform);
		RectTransform fruitRt = currentBomb.transform as RectTransform;
		int fruitX = Random.Range (minX, maxX); 
		fruitRt.anchoredPosition3D = new Vector3 (fruitX, fruitY, 0);
		fruitRt.localScale = new Vector3 (1, 1, 1);
		Rigidbody2D rigid2d = currentBomb.GetComponent<Rigidbody2D> ();
		forceX = Random.Range(3, 10) * 400;
		forceY = Random.Range (6, 8) * 1500;
		if (fruitX > 0) {
			force = new Vector2(-forceX, forceY);
		} else {
			force = new Vector2(forceX, forceY);
		}
		rigid2d.AddForce (force);
			
	}

	private void createMulBomb(){
		int ifCreateBomb = Random.Range (0, 9);
		if (ifCreateBomb < 3) {
			int num = Random.Range(1,3);
			for(int i = 0; i < num; i++){
				createBomb();
			}
		}
	}

	private void cutBomb(Vector2 pos){
		CancelInvoke ();
		KinectManager.Instance.enabled = false;
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Fruit");
		for (int i = 0; i < gameObjects.Length; i++) {
			gameObjects[i].GetComponent<DetectIfCut>().needDestroy = true;
		}
		cutBombReflect(pos);
		CameraShake.Instance.shake ();
		Invoke ("destroyTag", 1.5f);
	}

	
	private void destroyTag(){
		bombBlowReflect.SetActive (false);
		KinectManager.Instance.enabled = true;
		InvokeRepeating ("generateFruits", 0.0f, 3.0f);
	}

	private void createLotsOfFruits(){
		InvokeRepeating ("createFruit", 0.0f, 0.2f);
	}

	private void stopFruitRepeating(){
		CancelInvoke("createFruit");
	}

	private void stopBombRepeating(){
		CancelInvoke("createBomb");
	}

	private void cutBombReflect(Vector2 pos)
	{
		bombBlowReflect.SetActive (true);
		RectTransform tipRtf = bombBlowReflect.GetComponent<bombBlowTip> ().bombBlowTipRtf;
		tipRtf.anchoredPosition = pos;
	}
	
	public static GamingState Instance{
		get{
			return instance;
		}
	}

}
