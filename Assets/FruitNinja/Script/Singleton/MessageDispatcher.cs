using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MessageDispatcher : MonoBehaviour {

	private List<Message> delayMessage = new List<Message>();
	private static MessageDispatcher instance = null;

	void Awake(){
		instance = this;
	}

	public static MessageDispatcher Instance{
		get{
			return instance;
		}
	}

	public void disCharge(Message message, GameObject receiver){
		StateMachine stateMachine = receiver.GetComponent<StateMachine> ();
		stateMachine.handleMessage (message);
	}

	public void dispatchMessage(float delay, EntityType receiver, MessageType msg, Vector2 bombPos, int score){
		Message message = new Message ();
		GameObject pReceiver = EntityManager.Instance.getEntityFromId(receiver);
		if (msg == MessageType.Msg_CutBomb) {
			message.bombPosition = bombPos;
		}
		if (msg == MessageType.Msg_GameOver) {
			message.score = score;
		}
		if (pReceiver != null) {
			message.delay = delay;
			message.msg = msg;
			message.receiver = receiver;
			if (delay <= 0.0) {
				disCharge (message, pReceiver);
			} else {
				float currentTime = Time.time;
				message.dispatchTime = currentTime + delay;
				addMessageToDelayset(delayMessage, message);
			}
		}
	}

	void Update(){
		if (delayMessage.Count != 0) {
			Message message = delayMessage[0];
			float currentTime = Time.time;
			if(message.dispatchTime < currentTime && message.dispatchTime > 0){
				GameObject pReveiver = EntityManager.Instance.getEntityFromId(message.receiver);
				disCharge (message, pReveiver);
				delayMessage.RemoveAt(0);
			}
		}
	}

	private List<Message> addMessageToDelayset(List<Message> list, Message message){
		if (list.Count != 0) {
			for(int i = 0; i < list.Count; i++){
				if(list[i].dispatchTime > message.dispatchTime){
					list.Insert(i,message);
					return list;
				}
			}
			list.Add (message);
			return list;
		} else {
			list.Add(message);
			return list;
		}
	}
}
