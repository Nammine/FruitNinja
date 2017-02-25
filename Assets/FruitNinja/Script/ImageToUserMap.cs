using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageToUserMap : MonoBehaviour 
{
	public RawImage kinectImg;
	void Update () 
	{
		bool isInit = KinectManager.Instance.IsInitialized ();
		if (isInit) 
		{
			if(kinectImg != null && kinectImg.texture == null)
			{
			    Texture2D kinectUseMap = KinectManager.Instance.GetUsersLblTex();
				kinectImg.texture = kinectUseMap;
				//kinectImg.SetNativeSize();
			}
		}
	}
}
