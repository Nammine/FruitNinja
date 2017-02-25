using UnityEngine;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface {

	private MessageDispatcher messageDispatcher;
	private float delay = 0.0f;
	private static GestureListener instance = null;
	public static bool ifDetectGesture = false;

	public static GestureListener Instance{
		get{
			return instance;
		}
	}

	void Awake(){
		instance = this;
	}

	public void UserDetected(long userId, int userIndex){
		KinectManager manager = KinectManager.Instance;
		messageDispatcher = MessageDispatcher.Instance;
		manager.DetectGesture (userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture (userId, KinectGestures.Gestures.SwipeRight);
		manager.DetectGesture (userId, KinectGestures.Gestures.Push);
	}

	public void UserLost(long userId, int userIndex){
	}

	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos){

	}

	public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos){
		if (ifDetectGesture) {
			if (gesture == KinectGestures.Gestures.SwipeRight) {
				messageDispatcher.dispatchMessage(delay, EntityType.FirstPanelEntity, MessageType.Msg_SwipeRight, new Vector2(0,0), 0);
				messageDispatcher.dispatchMessage(delay, EntityType.GameOverEntity, MessageType.Msg_SwipeRight, new Vector2(0,0), 0);
			} else if(gesture == KinectGestures.Gestures.SwipeLeft){
				messageDispatcher.dispatchMessage(delay, EntityType.FirstPanelEntity, MessageType.Msg_SwipeLeft, new Vector2(0,0), 0);
				messageDispatcher.dispatchMessage(delay, EntityType.GameOverEntity, MessageType.Msg_SwipeLeft, new Vector2(0,0), 0);
			} else if (gesture == KinectGestures.Gestures.Push) {
				messageDispatcher.dispatchMessage(delay, EntityType.FirstPanelEntity, MessageType.Msg_Push, new Vector2(0,0), 0);
				messageDispatcher.dispatchMessage(delay, EntityType.GameOverEntity, MessageType.Msg_Push, new Vector2(0,0), 0);
			}
		}
		return true;
	}

	public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint){
		return true;
	}

}
