using UnityEngine;
using System.Collections;

public struct Message
{  
	public MessageType msg;  
	public float delay; 
	public float dispatchTime;
	public EntityType receiver;
	public int score;
	public Vector2 bombPosition;
}