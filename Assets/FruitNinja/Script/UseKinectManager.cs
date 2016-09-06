using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UseKinectManager : MonoBehaviour {

	private Canvas canvas;
	//public Image rightHand;
	//public Sprite[] handStateSprites;
	private PanelCenter panelCenter;
	public Image button1;
	public Image button2;
	public Image button3;
	public Image circle1;
	public Image circle2;
	public Image circle3;
	public int upForce = 2000;
	public int gravityScale = 10;
	private ImageToUserMap imageToUserMap;
	public RawImage kinectImg;
	private Image curBtn;//记录切中的水果，希望知道这个水果什么时候出界
	private int curBtnOutY = -300;
	public AudioClip start;
	public AudioClip menu;
	AudioSource sound;
	// Use this for initialization
	void Start () {
		GameObject canvasObj = GameObject.FindWithTag("Canvas");
		canvas = canvasObj.GetComponent<Canvas> ();
		panelCenter = canvasObj.GetComponent<PanelCenter> ();
		imageToUserMap = canvasObj.GetComponent<ImageToUserMap> ();
		imageToUserMap.kinectImg = kinectImg;
		sound = gameObject.GetComponent<AudioSource> ();
		sound.clip = menu;
		sound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		    //print ("x=" + KinectManager.Instance.GetDepthImageWidth()+ " y=" + KinectManager.Instance.GetDepthImageHeight());
			if (KinectManager.Instance.IsUserDetected ()) {
			//检测到玩家

			long userId = KinectManager.Instance.GetPrimaryUserID ();//获取玩家id
			//panelCenter.showGame2Panel();
			//Destroy(gameObject);
			//Vector2 userPos = KinectManager.Instance.GetUserPosition(userId);//获取整个玩家相对于体感的坐标信息
			int jointType = (int)KinectInterop.JointType.HandRight;//表示右手
       
			if (KinectManager.Instance.IsJointTracked (userId, jointType)) {
				//追踪到关节点
			
				Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointType);//获取右手信息
				Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);//右手转换到屏幕坐标
				Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);//三维坐标转换到二维
				Vector2 HandUguiPos;

				if (RectTransformUtility.ScreenPointToLocalPointInRectangle ((RectTransform)canvas.transform, HandScrPos, Camera.main, out HandUguiPos)) {
					//表示右手在canvas所表示的矩形范围内
					//RectTransform rightRectf = rightHand.transform as RectTransform;
					//rightRectf.anchoredPosition = HandUguiPos;//将表示右手的图片位置定位到手的位置
						                                             
				}

				//获取右手的姿势
				bool isHandClosed = false;
				//rightHand.sprite = handStateSprites [0];
				if (KinectManager.Instance.GetRightHandState (userId) == KinectInterop.HandState.Closed) {
					isHandClosed = true;
					//rightHand.sprite = handStateSprites [1];
				}
				
				if (isHandClosed) {
					if (circle1.enabled == true && RectTransformUtility.RectangleContainsScreenPoint (circle1.rectTransform, HandScrPos, Camera.main)) {
						//检测到点在rect中
						HandClickFruit(button1);

					} else if (circle2.enabled == true && RectTransformUtility.RectangleContainsScreenPoint (circle2.rectTransform, HandScrPos, Camera.main)) {
						//检测到点在rect中
						HandClickFruit(button2);
					} else if (circle3.enabled == true && RectTransformUtility.RectangleContainsScreenPoint (circle3.rectTransform, HandScrPos, Camera.main)) {
						//检测到点在rect中
						HandClickFruit(button3);
					}
				}


			} 
		}
		deteCurBtn ();
		}
	private void deteCurBtn(){
		if (curBtn != null) {
			RectTransform rtf = curBtn.transform as RectTransform;
			if(rtf.anchoredPosition.y < curBtnOutY){
				if(curBtn == button2){
					panelCenter.showGamePanel ();
				}
				else if(curBtn == button3){
					Application.Quit();
				}
				else if(curBtn == button1){
					panelCenter.showGame2Panel ();
				}
				Destroy (gameObject);
			}
		}
	}

	//切到水果的处理函数
	private void HandClickFruit(Image clickFruit){
		Rigidbody2D r1 = button1.GetComponent<Rigidbody2D> ();
		Rigidbody2D r2 = button2.GetComponent<Rigidbody2D> ();
		Rigidbody2D r3 = button3.GetComponent<Rigidbody2D> ();
		r1.gravityScale = gravityScale;
		r2.gravityScale = gravityScale;
		r3.gravityScale = gravityScale;
		circle1.enabled = false;
		circle2.enabled = false;
		circle3.enabled = false;

		curBtn = clickFruit;


		if (clickFruit == button1) {
			r1.AddForce (new Vector2 (0, upForce));
			if(audio.isPlaying){
				print ("执行到这一步");
				audio.Stop();
				PlayMusic(start);
			}
		} else if (clickFruit == button2) {
			r2.AddForce (new Vector2 (0, upForce));
			if(audio.isPlaying){
				//print ("执行到这一步");
				audio.Stop();
				PlayMusic(start);
			}
		} else {
			r3.AddForce (new Vector2 (0, upForce));
		}

	}
	private void PlayMusic(AudioClip music){
		sound.clip = music;
		sound.Play ();
	}

}
