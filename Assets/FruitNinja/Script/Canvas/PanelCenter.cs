using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelCenter : MonoBehaviour {
	public RectTransform firstPanelColone;
	public RectTransform gamePanelColone;
	public RectTransform gameOverPanelColone;
	public RectTransform game2PanelColone;
	public int scoreNum = 0;
	public int gameType = 1;
	private StateMachine stateMachine;
	private static PanelCenter instance = null;
	private GameObject canvasObj;
	
	void Start () {
		canvasObj = gameObject;
		EntityManager.Instance.registerEntity (canvasObj, EntityType.CanvasEntity);
		instance = this;
		stateMachine = gameObject.GetComponent<StateMachine> ();
		stateMachine.currentState = FirstPanelState.Instance;
		stateMachine.currentState.enter ();
		stateMachine.previousState = null;
	}

	void Update () {
//		currentState.execute ();
	}

	private RectTransform createFirstPanel () {
		RectTransform firstPanel = Instantiate (firstPanelColone, firstPanelColone.position, firstPanelColone.rotation) as RectTransform;
		firstPanel.SetParent(transform);
		firstPanel.anchoredPosition3D = new Vector3 (0, 0, 0);
		firstPanel.localScale = new Vector3 (1, 1, 1);
		firstPanel.offsetMin = new Vector2 (0, 0);
		firstPanel.offsetMax = new Vector2 (0,0);
		return firstPanel;
	}

	private RectTransform createGamePanel(){
		RectTransform gamePanel = Instantiate (gamePanelColone, gamePanelColone.position, gamePanelColone.rotation) as RectTransform;
		gamePanel.SetParent(transform);
		gamePanel.anchoredPosition3D = new Vector3 (0, 0, 0);
		gamePanel.localScale = new Vector3 (1, 1, 1);
		gamePanel.offsetMin = new Vector2 (0, 0);
		gamePanel.offsetMax = new Vector2 (0,0);
		return gamePanel;
	}

	private RectTransform createGameOverPanel(){
		RectTransform gameOverPanel = Instantiate (gameOverPanelColone, gameOverPanelColone.position, gameOverPanelColone.rotation) as RectTransform;
		gameOverPanel.SetParent(transform);
		gameOverPanel.anchoredPosition3D = new Vector3 (0, 0, 0);
		gameOverPanel.localScale = new Vector3 (1, 1, 1);
		gameOverPanel.offsetMin = new Vector2 (0, 0);
		gameOverPanel.offsetMax = new Vector2 (0,0);
		return gameOverPanel;
	}

	private RectTransform createGame2Panel(){
		RectTransform game2Panel = Instantiate (game2PanelColone, game2PanelColone.position, game2PanelColone.rotation) as RectTransform;
		game2Panel.SetParent(transform);
		game2Panel.anchoredPosition3D = new Vector3 (0, 0, 0);
		game2Panel.localScale = new Vector3 (1, 1, 1);
		game2Panel.offsetMin = new Vector2 (0, 0);
		game2Panel.offsetMax = new Vector2 (0,0);
		return game2Panel;
	}
	public void showGamePanel(){
		EntityManager.Instance.registerEntity (createGamePanel ().gameObject, EntityType.LifeGamePanelEntity);
		Singleton.gameType = EntityType.LifeGamePanelEntity;
	}

	public void showFirstPanel(){
		EntityManager.Instance.registerEntity (createFirstPanel ().gameObject, EntityType.FirstPanelEntity);
	} 
	public void showGameOverPanel(){
		EntityManager.Instance.registerEntity (createGameOverPanel ().gameObject, EntityType.GameOverEntity);
	}
	public void showGame2Panel(){
		EntityManager.Instance.registerEntity (createGame2Panel ().gameObject, EntityType.TimeGamePanelEntity);
		Singleton.gameType = EntityType.TimeGamePanelEntity;
	} 

	public void changeState(State newState){
		stateMachine.changeState (newState);
	}

	public void revertToPreviousState(){
		stateMachine.revertToPreviousState ();
	}


	public static PanelCenter Instance{
		get{
			return instance;
		}
	}

}
