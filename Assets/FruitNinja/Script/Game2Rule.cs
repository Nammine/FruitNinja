using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game2Rule : MonoBehaviour
{
	public RawImage kinectImg;
	private static Game2Rule instance = null;
	public bool[] ifFirstRightHandPos;
	public bool[] ifFirstLeftHandPos;
	public GameObject[] leftTrail;
	public GameObject[] rightTrail;
	public GameObject trailClone;
	public GameObject[] rightHandPosCollider;
	public GameObject[] leftHandPosCollider;
	public HandPosVelocity[] rightHandVelocity;
	public HandPosVelocity[] leftHandVelocity;
	public Collider2D[] rightHandCollider2D;
	public Collider2D[] leftHandCollider2D;
	public Image[] rightHandImage;
	public Image[] leftHandImage;
	public RectTransform[] rightHandRtf;
	public RectTransform[] leftHandRtf;
	public Vector2[] lastRightHandPos;
	public Vector2[] lastLeftHandPos;
	public Text gameStart;
	private StateMachine stateMachine;
	private GameObject canvasObj;
	public GameObject colliderColone;
	public GameObject lifeContent;


	void Start ()
	{
		instance = this;
		canvasObj = GameObject.FindWithTag("Canvas");
		stateMachine = gameObject.GetComponent<StateMachine> ();
		stateMachine.currentState = GameBeginState.Instance;
		stateMachine.currentState.enter ();
		stateMachine.previousState = null;
	}

	public void changeState(State newState){
		
		stateMachine.changeState (newState);
	}
	
	public void revertToPreviousState(){
		stateMachine.revertToPreviousState ();
	}

	
	void Update ()
	{
		if (KinectManager.Instance.IsUserDetected ()) {
			List<long> players = new List<long> ();
			players = KinectManager.Instance.GetUsersIdByNum (Singleton.playerNumber);
			for(int i = 0; i < Singleton.playerNumber; i++){
				if(i < players.Count){
					rightHandCollider2D[i].enabled = true;
					leftHandCollider2D[i].enabled = true;
					rightHandImage[i].enabled = true;
					leftHandImage[i].enabled = true;
				}
				else{
					rightHandCollider2D[i].enabled = false;
					leftHandCollider2D[i].enabled = false;
					rightHandImage[i].enabled = false;
					leftHandImage[i].enabled = false;
				}
			}
			int jointRightType = (int)KinectInterop.JointType.HandRight;
			int jointLeftType = (int)KinectInterop.JointType.HandLeft;
			foreach (long userId in players) {
				int playerId = players.IndexOf(userId);
				if (KinectManager.Instance.IsJointTracked (userId, jointRightType)) {
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointRightType);
					Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);
					Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);
					Vector2 HandCanvasPos;
					if(RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)(canvasObj.transform),HandScrPos,Camera.main,out HandCanvasPos)){
						rightHandRtf[playerId].anchoredPosition = HandCanvasPos;
						if(ifFirstRightHandPos[playerId]){
							rightHandVelocity[playerId].velocity = 0f;
							ifFirstRightHandPos[playerId] = false;
						}
						else {
							rightHandVelocity[playerId].velocity = System.Convert.ToSingle(Vector2.Distance(lastRightHandPos[playerId], HandCanvasPos)) / Time.deltaTime;
						}
						lastRightHandPos[playerId] = HandCanvasPos;
					}
					rightTrail [players.IndexOf (userId)].transform.position = HandPos;
				} 
				if (KinectManager.Instance.IsJointTracked (userId, jointLeftType)) {
					Vector3 HandPos = KinectManager.Instance.GetJointKinectPosition (userId, jointLeftType);
					Vector3 HandScreenPos = Camera.main.WorldToScreenPoint (HandPos);
					Vector2 HandScrPos = new Vector2 (HandScreenPos.x, HandScreenPos.y);
					Vector2 HandCanvasPos;
					if(RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)(canvasObj.transform),HandScrPos,Camera.main,out HandCanvasPos)){
						leftHandRtf[playerId].anchoredPosition = HandCanvasPos;
						if(ifFirstLeftHandPos[playerId]){
							leftHandVelocity[playerId].velocity = 0f;
							ifFirstLeftHandPos[playerId] = false;
						}
						else {
							leftHandVelocity[playerId].velocity = System.Convert.ToSingle(Vector2.Distance(lastLeftHandPos[playerId], HandCanvasPos)) / Time.deltaTime;
						}
						lastLeftHandPos[playerId] = HandCanvasPos;
					}
					leftTrail [players.IndexOf (userId)].transform.position = HandPos;
				}
			}

		}
	}
	public static Game2Rule Instance{
		get{
			return instance;
		}
	}
}
