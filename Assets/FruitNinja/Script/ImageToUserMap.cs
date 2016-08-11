using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageToUserMap : MonoBehaviour {

	public RawImage kinectImg;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool isInit = KinectManager.Instance.IsInitialized ();
		if (isInit) {
		//设备初始化完成
			if(kinectImg != null && kinectImg.texture == null){
				//Texture2D KinectPic = KinectManager.Instance.GetUsersClrTex();//获取彩色数据流
			    Texture2D kinectUseMap = KinectManager.Instance.GetUsersLblTex();//获取深度数据流
				kinectImg.texture = kinectUseMap;//设备显示深度数据流
			}
	}
	}
}
