using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LifeContent : MonoBehaviour {
	public Toggle life1;
	public Toggle life2;
	public Toggle life3;
	public int lifeNum = 3;
	private static LifeContent instance = null;
	void Awake(){
		instance = this;
	}
	void Update(){
		setLife (lifeNum);
		if (lifeNum == 0) {
			MessageDispatcher.Instance.dispatchMessage (0.0f, EntityType.LifeGamePanelEntity, MessageType.Msg_GameOver, new Vector2(0,0), 0);
		}
	}
	public void setLife(int lifeNum){
		if (lifeNum == 3) {
			life1.isOn = true;
			life2.isOn = true;
			life3.isOn = true;
		} else if (lifeNum == 2) {
			life1.isOn = false;
			life2.isOn = true;
			life3.isOn = true;
		} else if (lifeNum == 1) {
		
			life1.isOn = false;
			life2.isOn = false;
			life3.isOn = true;
		} else {

			life1.isOn = false;
			life2.isOn = false;
			life3.isOn = false;
		}
	}
	public static LifeContent Instance{
		get{
			return instance;
		}
	}
}
