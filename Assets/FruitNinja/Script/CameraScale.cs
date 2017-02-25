using UnityEngine;
using System.Collections;

public class CameraScale : MonoBehaviour {
	//屏幕自适应
	void Awake(){
		CameraScaleAdjust ();
		FullScreenSet ();
	}
	public void CameraScaleAdjust(){
		int ManualWidth = 640;
		int ManualHeight = 480;
//		int manualHeight;
		float scaleOfDis;
		if (System.Convert.ToSingle (Screen.height) / Screen.width > System.Convert.ToSingle (ManualHeight) / ManualWidth) {
//			manualHeight = Mathf.RoundToInt (System.Convert.ToSingle (Screen.width) / ManualWidth * Screen.height);
			scaleOfDis = System.Convert.ToSingle(Screen.height) / ManualHeight;
		} else {
// 			manualHeight = ManualHeight;
			scaleOfDis = System.Convert.ToSingle(Screen.width) / ManualWidth;
		}
//		Camera camera = GetComponent<Camera> ();
//		float scale = System.Convert.ToSingle (manualHeight / 480f);
//		camera.fieldOfView *= scale;
		SingularValue.minDistance *= scaleOfDis;
	}
	public void FullScreenSet(){
		Resolution[] resolutions = Screen.resolutions;
		Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height,true);  
		Screen.fullScreen = true;
	}

}
