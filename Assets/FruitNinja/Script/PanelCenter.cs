using UnityEngine;
using System.Collections;

public class PanelCenter : MonoBehaviour {
	public RectTransform firstPanelColone;
	public RectTransform gamePanelColone;
	public RectTransform gameOverPanelColone;
	public RectTransform game2PanelColone;
	public int scoreNum = 0;


	// Use this for initialization
	void Start () {
		createFirstPanel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void createFirstPanel(){
		RectTransform firstPanel = Instantiate (firstPanelColone, firstPanelColone.position, firstPanelColone.rotation) as RectTransform;
		firstPanel.SetParent(transform);
		firstPanel.anchoredPosition3D = new Vector3 (0, 0, 0);
		firstPanel.localScale = new Vector3 (1, 1, 1);
		firstPanel.offsetMin = new Vector2 (0, 0);
		firstPanel.offsetMax = new Vector2 (0,0);
	}

	private void createGamePanel(){
		RectTransform gamePanel = Instantiate (gamePanelColone, gamePanelColone.position, gamePanelColone.rotation) as RectTransform;
		gamePanel.SetParent(transform);
		gamePanel.anchoredPosition3D = new Vector3 (0, 0, 0);
		gamePanel.localScale = new Vector3 (1, 1, 1);
		gamePanel.offsetMin = new Vector2 (0, 0);
		gamePanel.offsetMax = new Vector2 (0,0);
	}

	private void createGameOverPanel(){
		RectTransform gameOverPanel = Instantiate (gameOverPanelColone, gameOverPanelColone.position, gameOverPanelColone.rotation) as RectTransform;
		gameOverPanel.SetParent(transform);
		gameOverPanel.anchoredPosition3D = new Vector3 (0, 0, 0);
		gameOverPanel.localScale = new Vector3 (1, 1, 1);
		gameOverPanel.offsetMin = new Vector2 (0, 0);
		gameOverPanel.offsetMax = new Vector2 (0,0);
	}

	private void createGame2Panel(){
		RectTransform game2Panel = Instantiate (game2PanelColone, game2PanelColone.position, game2PanelColone.rotation) as RectTransform;
		game2Panel.SetParent(transform);
		game2Panel.anchoredPosition3D = new Vector3 (0, 0, 0);
		game2Panel.localScale = new Vector3 (1, 1, 1);
		game2Panel.offsetMin = new Vector2 (0, 0);
		game2Panel.offsetMax = new Vector2 (0,0);
	}
	public void showGamePanel(){
		createGamePanel ();
	}

	public void showFirstPanel(){
		createFirstPanel();
	} 
	public void showGameOverPanel(){
		createGameOverPanel();
	} 

	public void showGame2Panel(){
		createGame2Panel();
	} 
}
