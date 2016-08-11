using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour {
	public Fruit fruitClone;
	public int forceX = 300;
	public int forceY = 7000;
	public int forceHalfY = 3000;
	private const int minX = -288;
	private const int maxX = 288;
	private const int fruitY = -275;
	private const int fruitOutY = -275;
	private Fruit currentFruit;
	public Splash splashClone;
	
	public Transform leftTrail;
	public Transform rightTrail;
	
	private int lifeNum = 3;
	private int scoreNum = 0;
	
	public LifeContent lifeContent;
	
	public Text scoreTxt;
	public Image gameOverImg;
	
	private PanelCenter panelCenter;
	public AudioClip slatter;
	public AudioClip boom;
	
	public RectTransform playOverColone;
	
	AudioSource sound;
	// Use this for initialization
	void Start () {
		createFruit ();
		gameOverImg.gameObject.SetActive (false);
		GameObject canvasObj = GameObject.FindWithTag("Canvas");
		//canvas = canvasObj.GetComponent<Canvas> ();
		panelCenter = canvasObj.GetComponent<PanelCenter> ();
		sound = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		bool needDestroyFruit = false;
		bool isOutDestroy = false;
		if (currentFruit != null) {
			if (KinectManager.Instance.IsUserDetected ()) {
				//检测到玩家
				
				long userId = KinectManager.Instance.GetPrimaryUserID ();//获取玩家id
				//Vector2 userPos = KinectManager.Instance.GetUserPosition(userId);//获取整个玩家相对于体感的坐标信息
				int jointRightType = (int)KinectInterop.JointType.HandRight;//表示右手
				
				if (KinectManager.Instance.IsJointTracked (userId, jointRightType)) {
					//追踪到关节点
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointRightType);//获取右手信息
					rightTrail.position = HandPos;
					Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
					Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
					//KinectInterop.HandState rightHandState = KinectManager.Instance.GetRightHandState(userId);rightHandState == KinectInterop.HandState.Open && 
					if(RectTransformUtility.RectangleContainsScreenPoint (currentFruit.transform as RectTransform, HandScrPos, Camera.main)){
						needDestroyFruit = true;
					}
				} 
				
				int jointLeftType = (int)KinectInterop.JointType.HandLeft;//表示左手
				
				if (KinectManager.Instance.IsJointTracked (userId, jointLeftType)) {
					//追踪到关节点
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointLeftType);//获取左手信息
					leftTrail.position = HandPos;
					Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
					Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
					//KinectInterop.HandState leftHandState = KinectManager.Instance.GetLeftHandState(userId);leftHandState == KinectInterop.HandState.Open && 
					if(RectTransformUtility.RectangleContainsScreenPoint (currentFruit.transform as RectTransform, HandScrPos, Camera.main)){
						needDestroyFruit = true;
					}
				} 
			}
			
			RectTransform rtf = currentFruit.transform as RectTransform;
			float curFruitY = rtf.anchoredPosition.y;//获取当前水果的y轴位置
			if(curFruitY < fruitOutY){
				needDestroyFruit = true;
				isOutDestroy = true;
			}
			if(needDestroyFruit){
				//切中水果或出界
				if(isOutDestroy){
					if(currentFruit.type == Contant.Type_Boom){
						scoreNum++;
					}
					else{
						lifeNum--;
					}
				}
				else{
					if(currentFruit.type == Contant.Type_Boom){
						lifeNum--;
					}
					else{
						scoreNum++;
					}
				}
			}
			lifeContent.setLife(lifeNum);
			scoreTxt.text = scoreNum+"";
			if(needDestroyFruit){
				if(isOutDestroy == false){
					if(currentFruit.type != Contant.Type_Boom ){
						//切得不是炸弹才会生成左右水果
						createRightLeftFruit();
						playMusic (slatter);
						Destroy(currentFruit.gameObject);
					}
					else{
						playMusic(boom);
						Destroy(currentFruit.gameObject);
					}
					
				}
				
				if(lifeNum > 0){
					createFruit();
				}
				else{
					if(GameObject.FindWithTag("PlayOver") == null){
						createPlayOver();
					}
					gameOverImg.gameObject.SetActive(true);
				}
				
			}
			
		}
		detectedClickGameOver ();
	}
	
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
				leftTrail.position = HandPos;
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
	private void createRightLeftFruit(){
		Fruit leftFruit = Instantiate (currentFruit, currentFruit.transform.position, currentFruit.transform.rotation) as Fruit;
		leftFruit.setType (currentFruit.type + 1);
		newFruitInit (leftFruit, true);
		Fruit rightFruit = Instantiate (currentFruit, currentFruit.transform.position, currentFruit.transform.rotation) as Fruit;
		rightFruit.setType (currentFruit.type + 2);
		newFruitInit (rightFruit, false);
		Splash sl = Instantiate (splashClone) as Splash;
		sl.setColor (currentFruit.type);
		newSplashInit (sl);
		Destroy (sl.gameObject,1f);
		print ("生成果汁");
	}
	private void newSplashInit(Splash s){
		
		
		RectTransform curRtf = currentFruit.transform as RectTransform;
		s.transform.SetParent (transform);
		RectTransform rtf = s.transform as RectTransform;
		rtf.anchoredPosition3D = new Vector3 (0, 0, 0);
		rtf.anchoredPosition = curRtf.anchoredPosition;
		rtf.localScale = new Vector3 (1, 1, 1);
	}
	private void newFruitInit(Fruit fruit,bool isLeft){
		
		RectTransform curRtf = currentFruit.transform as RectTransform;
		fruit.transform.SetParent (transform);
		RectTransform rtf = fruit.transform as RectTransform;
		rtf.anchoredPosition3D = new Vector3 (0, 0, 0);
		rtf.anchoredPosition = curRtf.anchoredPosition;
		rtf.localScale = new Vector3 (1, 1, 1);
		
		Rigidbody2D rb2d = fruit.GetComponent<Rigidbody2D> ();
		if (isLeft) {
			rb2d.AddForce (new Vector2 (-forceX, forceHalfY));
		} else {
			rb2d.AddForce(new Vector2(forceX,forceHalfY));
		}
		print ("创建左右水果");
	}
	private void createMulFruit(){
		int number = Random.Range (1, 8);
		for (int i = 0; i<number; i++) {
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
	private void playMusic(AudioClip music){
		sound.clip = music;
		sound.pitch = 1;
		sound.volume = 1;
		sound.Play ();
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
