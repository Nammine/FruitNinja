using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;


public class Singleton : MonoBehaviour  
{  
	private static GameObject m_Container = null;  
	private static string m_Name = "Singleton";  
	private static Dictionary<string, object> m_SingletonMap = new Dictionary<string, object>();  
	private static bool m_IsDestroying = false;
	public static int gravityScale = 10;
	public static int upForce = 3000;
	public static int curBtnOutY = -300;
	public static int playerNumber = 1;
	public static EntityType gameType;
	
	public static bool IsDestroying  
	{  
		get { return m_IsDestroying; }  
	}  
	
	public static bool IsCreatedInstance(string Name)  
	{  
		if(m_Container == null)  
		{  
			return false;  
		}  
		if (m_SingletonMap!=null && m_SingletonMap.ContainsKey(Name))   
		{  
			return true;  
		}  
		return false;  
		
	}  
	public static object getInstance (string Name)  
	{  
		if(m_Container == null)  
		{  
			Debug.Log("Create Singleton.");  
			m_Container = new GameObject ();  
			m_Container.name = m_Name;
			m_Container.tag = "Singleton";
			m_Container.AddComponent (typeof(Singleton));  
		}  
		if (!m_SingletonMap.ContainsKey(Name)) {  
			if(System.Type.GetType(Name) != null)  
			{  
				m_SingletonMap.Add(Name, m_Container.AddComponent (System.Type.GetType(Name)));  
			}  
			else  
			{  
				Debug.LogWarning("Singleton Type ERROR! (" + Name + ")");  
			}  
		}  
		return m_SingletonMap[Name];  
	}     
	
	public static void RemoveInstance(string Name)  
	{  
		if (m_Container != null && m_SingletonMap.ContainsKey(Name))  
		{  
			UnityEngine.Object.Destroy((UnityEngine.Object)(m_SingletonMap[Name]));  
			m_SingletonMap.Remove(Name);  
			
			Debug.LogWarning("Singleton REMOVE! (" + Name + ")");  
		}  
	}  
	
	void Awake ()  
	{  
		Debug.Log("Awake Singleton.");  
		DontDestroyOnLoad (gameObject);  
	}  
	
	void Start()  
	{  
		Debug.Log("Start Singleton.");  
	}     
	
	void Update()  
	{  
	}  
	
	void OnApplicationQuit()  
	{  
		Debug.Log("Destroy Singleton");  
		if(m_Container != null)  
		{  
			GameObject.Destroy(m_Container);  
			m_Container = null;  
			m_IsDestroying = true;  
		}             
	}

	public static void playMusic(AudioClip clip, AudioSource source){
		source.clip = clip;
		source.Play ();
	}

	public static void WindowModeChange ()
	{
		Resolution[] resolutions = Screen.resolutions;
		Screen.SetResolution (resolutions [resolutions.Length - 1].width, resolutions [resolutions.Length - 1].height, false);  
		Screen.fullScreen = false;
	}
	
	public static void FullScreenSet ()
	{
		Resolution[] resolutions = Screen.resolutions;
		Screen.SetResolution (resolutions [resolutions.Length - 1].width, resolutions [resolutions.Length - 1].height, true);  
		Screen.fullScreen = true;
	}
	
}  