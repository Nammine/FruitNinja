using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandPosVelocity : MonoBehaviour {
	public float velocity;	

	void Awake(){
		gameObject.GetComponent<CircleCollider2D> ().radius = SingularValue.radius;
		RectTransform rtf = gameObject.transform as RectTransform;
		rtf.sizeDelta = new Vector2 (SingularValue.x, SingularValue.y);
	}
	void Update(){
		//Debug.Log ("velocity" + velocity);
	}
}
