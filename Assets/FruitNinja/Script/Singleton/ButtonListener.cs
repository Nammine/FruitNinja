using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.FirstPanelEntity, MessageType.Msg_Left, new Vector2 (0, 0), 0);
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.GameOverEntity, MessageType.Msg_Left, new Vector2 (0, 0), 0);
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.FirstPanelEntity, MessageType.Msg_Right, new Vector2 (0, 0), 0);
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.GameOverEntity, MessageType.Msg_Right, new Vector2 (0, 0), 0);
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.FirstPanelEntity, MessageType.Msg_Space, new Vector2 (0, 0), 0);
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.GameOverEntity, MessageType.Msg_Space, new Vector2 (0, 0), 0);
		} else if (Input.GetKeyUp (KeyCode.Tab)) {
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.FirstPanelEntity, MessageType.Msg_Tab, new Vector2 (0, 0), 0);
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.GameOverEntity, MessageType.Msg_Tab, new Vector2 (0, 0), 0);
		} else if (Input.GetKeyUp (KeyCode.Escape)) {
			MessageDispatcher.Instance.dispatchMessage(0.0f, EntityType.FirstPanelEntity, MessageType.Msg_Esc, new Vector2(0,0), 0);
			MessageDispatcher.Instance.dispatchMessage(0.0f, EntityType.GameOverEntity, MessageType.Msg_Esc, new Vector2(0,0), 0);
		}
	}
}
