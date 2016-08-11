using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	private PanelCenter panelCenter;
	public Image gameOverImg;
	public Image passImg;
	public Text scoreTxt;
	private Image img;
	void Start(){
		GameObject canvasObj = GameObject.FindWithTag("Canvas");
		panelCenter = canvasObj.GetComponent<PanelCenter> ();
		showScore ();
		showImg ();
	}
	void Update(){
		if (panelCenter.scoreNum > 10) {
			detectedClickPass ();
		} else {
			detectedClickGameOver ();
		}
	}
	private void detectedClickGameOver(){
		if (KinectManager.Instance.IsUserDetected ()) {
			//检测到玩家
			long userId = KinectManager.Instance.GetPrimaryUserID ();//获取玩家id
			//Vector2 userPos = KinectManager.Instance.GetUserPosition(userId);//获取整个玩家相对于体感的坐标信息
			int jointLeftType = (int)KinectInterop.JointType.HandLeft;//表示右手
			
			if (KinectManager.Instance.IsJointTracked (userId, jointLeftType)) {
				//追踪到关节点
				Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointLeftType);//获取右手信息
				//leftTrail.position = HandPos;
				Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
				Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
				KinectInterop.HandState leftHandState = KinectManager.Instance.GetLeftHandState (userId);
				if (leftHandState == KinectInterop.HandState.Closed && RectTransformUtility.RectangleContainsScreenPoint (gameOverImg.transform as RectTransform, HandScrPos, Camera.main)) {
					Destroy(gameObject);
					panelCenter.showFirstPanel();
				}
			} 
		}
	}
	private void detectedClickPass(){
		if (KinectManager.Instance.IsUserDetected ()) {
			//检测到玩家
			long userId = KinectManager.Instance.GetPrimaryUserID ();//获取玩家id
			//Vector2 userPos = KinectManager.Instance.GetUserPosition(userId);//获取整个玩家相对于体感的坐标信息
			int jointLeftType = (int)KinectInterop.JointType.HandLeft;//表示右手
			
			if (KinectManager.Instance.IsJointTracked (userId, jointLeftType)) {
				//追踪到关节点
				Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointLeftType);//获取右手信息
				//leftTrail.position = HandPos;
				Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
				Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
				KinectInterop.HandState leftHandState = KinectManager.Instance.GetLeftHandState (userId);
				if (leftHandState == KinectInterop.HandState.Closed && RectTransformUtility.RectangleContainsScreenPoint (passImg.transform as RectTransform, HandScrPos, Camera.main)) {
					Destroy(gameObject);
					panelCenter.showFirstPanel();
				}
			} 
		}
	}
	private void showScore(){
		scoreTxt.text = panelCenter.scoreNum + "";
		if (panelCenter.scoreNum > 10) {
			scoreTxt.color = Color.blue;
		} else 
			scoreTxt.color = Color.red;
	}
	private void showImg(){
		if (panelCenter.scoreNum > 10) {
			gameOverImg.gameObject.SetActive (false);
		} else {
			passImg.gameObject.SetActive (false);
		}
	}
}
