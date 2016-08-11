using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UseKinectGesture : MonoBehaviour,KinectGestures.GestureListenerInterface {
	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	 
	}


	#region GestureListenerInterface implementation
	public void UserDetected (long userId, int userIndex)
	{
		text.text += "检测到用户";
	}
	public void UserLost (long userId, int userIndex)
	{
		text.text += "用户离开";
	}
	public void GestureInProgress (long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{

	}
	public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
	{
		if (gesture == KinectGestures.Gestures.Push) {
			text.text += "做push";
		}

		if (gesture == KinectGestures.Gestures.SwipeRight) {
			text.text += "做SwipeRight";
		}

		return true;
	}
	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
	{
		return true;
	}
	#endregion
}
