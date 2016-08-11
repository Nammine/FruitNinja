using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectFr : MonoBehaviour {
	public HalfFruit fruitClone;
	private GameRule gameRule;
	private LifeNumControl lifeNumControl;
	private ScoreControl scoreControl;
	public int forceX = 300;
	public int forceY = 7000;
	public int forceHalfY = 3000;
	private const int minX = -288;
	private const int maxX = 288;
	private const int fruitY = -275;
	private const int fruitOutY = -275;
	public Splash splashClone;
	//private int lifeNum = 3;
	//private int scoreNum = 0;
	public AudioClip slatter;
	public AudioClip boom;
	AudioSource sound;
	// Use this for initialization
	void Start () {
		//GameObject canvasObj = GameObject.FindWithTag("Canvas");
		GameObject gamePanelObj = GameObject.FindWithTag("Game");
		GameObject lifeControlObj = GameObject.FindWithTag("LifeControl");
		lifeNumControl = lifeControlObj.GetComponent<LifeNumControl> ();
		scoreControl = lifeControlObj.GetComponent<ScoreControl> ();
		//canvas = canvasObj.GetComponent<Canvas> ();
		gameRule = gamePanelObj.GetComponent<GameRule> ();
		sound = gamePanelObj.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool needDestroyFruit = false;
		bool isOutDestroy = false;

		    /*判断是否切中水果*/
			if (KinectManager.Instance.IsUserDetected ()) {
				//检测到玩家
				long userId = KinectManager.Instance.GetPrimaryUserID ();//获取玩家id
				//Vector2 userPos = KinectManager.Instance.GetUserPosition(userId);//获取整个玩家相对于体感的坐标信息

				int jointRightType = (int)KinectInterop.JointType.HandRight;//表示右手
				if (KinectManager.Instance.IsJointTracked (userId, jointRightType)) {
					//追踪到关节点
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointRightType);//获取右手信息
					Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
					Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
					//KinectInterop.HandState rightHandState = KinectManager.Instance.GetRightHandState(userId);rightHandState == KinectInterop.HandState.Open && 
					if(RectTransformUtility.RectangleContainsScreenPoint (gameObject.transform as RectTransform, HandScrPos, Camera.main)){
						needDestroyFruit = true;
					}
				} 
				
				int jointLeftType = (int)KinectInterop.JointType.HandLeft;//表示左手
				if (KinectManager.Instance.IsJointTracked (userId, jointLeftType)) {
					//追踪到关节点
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointLeftType);//获取左手信息
					Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
					Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
					//KinectInterop.HandState leftHandState = KinectManager.Instance.GetLeftHandState(userId);leftHandState == KinectInterop.HandState.Open && 
					if(RectTransformUtility.RectangleContainsScreenPoint (gameObject.transform as RectTransform, HandScrPos, Camera.main)){
						needDestroyFruit = true;
					}
				} 
			}
			
		/*判断是否出界*/
			RectTransform rtf = gameObject.transform as RectTransform;
			float curFruitY = rtf.anchoredPosition.y;//获取当前水果的y轴位置
			if(curFruitY < fruitOutY){
				needDestroyFruit = true;
				isOutDestroy = true;
			}

		/*计分*/
			if(needDestroyFruit){
				//切中水果或出界
				if(isOutDestroy){
					if(gameObject.GetComponent<Fruit>().type == Contant.Type_Boom){
						gameRule.scoreNum++;
					     scoreControl.changeScore();
					    
					}
					else{
						gameRule.lifeNum--;
					   lifeNumControl.changeLifeNum();
					}
				}
				else{
				if(gameObject.GetComponent<Fruit>().type == Contant.Type_Boom){
						gameRule.lifeNum--;
					    lifeNumControl.changeLifeNum();
					}
					else{
						gameRule.scoreNum++;
					      scoreControl.changeScore();					      
					}
				}
			}

			//gameRule.lifeContent.setLife(gameRule.lifeNum);
			//gameRule.scoreTxt.text = gameRule.scoreNum+"";
		     gameRule.lifeContent.setLife (lifeNumControl.lifeNum);
		     gameRule.scoreTxt.text = scoreControl.score+"";

		/*生成两半水果并且销毁水果*/
			if(needDestroyFruit){
				if(isOutDestroy == false){
					if(gameObject.GetComponent<Fruit>().type != Contant.Type_Boom ){
						//切得不是炸弹才会生成左右水果
						createRightLeftFruit();
						playMusic (slatter);
						Destroy(gameObject);
				
					}
					else{
						playMusic(boom);
						Destroy(gameObject);

					}
					
				}
			   else{
				       Destroy(gameObject);
			   }
				
				/*if(lifeNum > 0){
					//createFruit();
				}
				else{
					if(GameObject.FindWithTag("PlayOver") == null){
						createPlayOver();
					}
					gameOverImg.gameObject.SetActive(true);
				}*/
				
			}
			
		

	}

	private void createRightLeftFruit(){
		HalfFruit leftFruit = Instantiate (fruitClone, transform.position, transform.rotation) as HalfFruit;
		leftFruit.setType (gameObject.GetComponent<Fruit>().type + 1);
		newFruitInit (leftFruit, true);
		HalfFruit rightFruit = Instantiate (fruitClone, transform.position, transform.rotation) as HalfFruit;
		rightFruit.setType (gameObject.GetComponent<Fruit>().type + 2);
		newFruitInit (rightFruit, false);
		Splash sl = Instantiate (splashClone) as Splash;
		sl.setColor (gameObject.GetComponent<Fruit>().type);
		newSplashInit (sl);
		Destroy (sl.gameObject,1f);
		print ("生成果汁");
	}

	private void newSplashInit(Splash s){
		RectTransform curRtf = transform as RectTransform;
		s.transform.SetParent (gameRule.transform);
		RectTransform rtf = s.transform as RectTransform;
		rtf.anchoredPosition3D = new Vector3 (0, 0, 0);
		rtf.anchoredPosition = curRtf.anchoredPosition;
		rtf.localScale = new Vector3 (1, 1, 1);
	}

	private void newFruitInit(HalfFruit fruit,bool isLeft){
		
		RectTransform curRtf = transform as RectTransform;
		fruit.transform.SetParent (gameRule.transform);
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

	private void playMusic(AudioClip music){
		sound.clip = music;
		sound.pitch = 1;
		sound.volume = 1;
		sound.Play ();
	}

}
