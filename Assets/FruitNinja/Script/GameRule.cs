using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRule : MonoBehaviour {
	public Fruit fruitClone;//水果预制
	public int forceX = 300;//x轴的力
	public int forceY = 7000;//y轴的力
	public int forceHalfY = 3000;//一半水果上抛的力
	private const int minX = -288;//panel的范围
	private const int maxX = 288;
	private const int fruitY = -275;//水果从什么位置抛出
	private const int fruitOutY = -275;//水果掉出界的位置**
	private Fruit currentFruit;//现在产生的水果
	//public int number;//产生多少个水果，随机数，用来判断是否产生多个水果
	public Transform leftTrail;//左手刀光
	public Transform rightTrail;//右手刀光
	public int lifeNum = 3;//生命值
	public int scoreNum = 0;//分数
	public LifeContent lifeContent;//界面叉叉的显示
	public Text scoreTxt;//分数显示
	public Image gameOverImg;//gameover图片
	private PanelCenter panelCenter;//控制显示哪个面板
	public RectTransform playOverColone;//播放游戏结束的声音
	private int num;
	public RectTransform lifeSemaphoreColone;
	private RectTransform lifeControl;
	//AudioSource sound;//声音**

	/* 1.产生多个水果
	 * 2.gameover图片不显示
       3.获取canvas
       4.获取canvas中的Panelcenter组件
       5.获取声音**
	 */
	void Start () {
		createMulFruit ();
		//gameOverImg.gameObject.SetActive (false);
		GameObject canvasObj = GameObject.FindWithTag("Canvas");
		lifeControl = Instantiate (lifeSemaphoreColone, lifeSemaphoreColone.position, lifeSemaphoreColone.rotation) as RectTransform;
		lifeControl.SetParent(transform);
		//canvas = canvasObj.GetComponent<Canvas> ();
		panelCenter = canvasObj.GetComponent<PanelCenter> ();
		//sound = gameObject.GetComponent<AudioSource> ();//**
	}
	

	void Update () {
		if (currentFruit != null) {
			//判断当前是否有水果，如果有的话执行下面的代码，如果没有就判断是否点击了gameover

			/*检测玩家与左右手位置*/
			if (KinectManager.Instance.IsUserDetected ()) {
				//检测到玩家
				long userId = KinectManager.Instance.GetPrimaryUserID ();//获取玩家id
				//Vector2 userPos = KinectManager.Instance.GetUserPosition(userId);//获取整个玩家相对于体感的坐标信息
				/*判断是否检测到右手*/
				int jointRightType = (int)KinectInterop.JointType.HandRight;//表示右手
				if (KinectManager.Instance.IsJointTracked (userId, jointRightType)) {
					//追踪到关节点
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointRightType);//获取右手信息
					rightTrail.position = HandPos;
					//Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
					//Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
					//KinectInterop.HandState rightHandState = KinectManager.Instance.GetRightHandState(userId);rightHandState == KinectInterop.HandState.Open && 
				} 
				/*判断是否检测到左手*/
				int jointLeftType = (int)KinectInterop.JointType.HandLeft;//表示左手
				if (KinectManager.Instance.IsJointTracked (userId, jointLeftType)) {
					//追踪到关节点
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointLeftType);//获取左手信息
					leftTrail.position = HandPos;
					//Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
					//Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
					//KinectInterop.HandState leftHandState = KinectManager.Instance.GetLeftHandState(userId);leftHandState == KinectInterop.HandState.Open && 

				} 
			}

			/*显示生命值与分数*/
			//lifeContent.setLife(lifeNum);
			//scoreTxt.text = scoreNum+"";

			/*判断生命值是否为0 如果为0 判断之前产生的水果number 是否为0 如果为0创建多个水果*/
			if(lifeControl.gameObject.GetComponent<LifeNumControl>().lifeNum > 0){
				if(!IsInvoking("createMulFruit")){
					Invoke ("createMulFruit",2);
				}
				panelCenter.scoreNum = lifeControl.gameObject.GetComponent<ScoreControl>().score;
					
			}
			else{
				/*
				if(GameObject.FindWithTag("PlayOver") == null){
						createPlayOver();
				}
				 gameOverImg.gameObject.SetActive(true);*/
				CancelInvoke("createMulFruit");
				panelCenter.showGameOverPanel();
				Destroy(gameObject);
			}

		}
		//detectedClickGameOver ();
	}
	/*
	private void detectedClickGameOver(){
		if (gameOverImg.gameObject.activeSelf == false) {
			return;
		}
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
	*/
	private void createMulFruit(){
		num = Random.Range (1, 4);
		//number = num;
		for (int i = 0; i<num; i++) {
			createFruit();
		}
	}

	private Fruit createFruit(){
		currentFruit = Instantiate (fruitClone) as Fruit;//调用预制克隆了一个物体 默认克隆出的物体是在游戏面板中
		currentFruit.transform.SetParent (transform);
		RectTransform fruitRt = currentFruit.transform as RectTransform;
		int fruitX = Random.Range (minX, maxX); 
		fruitRt.anchoredPosition3D = new Vector3 (fruitX, fruitY, 0);//水果在随机位置产生
		fruitRt.localScale = new Vector3 (1, 1, 1);
		int[] types = {
			Contant.Type_Boom ,
			Contant.Type_Apple ,
			Contant.Type_Banana ,
			Contant.Type_Basaha,
			Contant.Type_Peach ,
			Contant.Type_Sandia
		};
		int type = Random.Range (0, 5);
		int fruitType = types[type];
		currentFruit.setType (fruitType);//随机生成了水果
		Rigidbody2D rigid2d = currentFruit.GetComponent<Rigidbody2D> ();
		//由水果的位置决定水果作用力的方向
		if (fruitX > 0) {
			rigid2d.AddForce(new Vector2(-forceX,forceY));
		} else {
			rigid2d.AddForce(new Vector2(forceX,forceY));
		}
		return currentFruit;

	}
	private void createPlayOver(){
		RectTransform playOver = Instantiate (playOverColone, playOverColone.position, playOverColone.rotation) as RectTransform;
		playOver.SetParent (transform);
		playOver.anchoredPosition3D = new Vector3 (0, 0, 0);
		playOver.localScale = new Vector3 (1, 1, 1);
		playOver.offsetMin = new Vector2 (0, 0);
		playOver.offsetMax = new Vector2 (0,0);
	}

}
