using UnityEngine;
using System.Collections;

public class AroundCollider: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//碰撞发生时会执行该函数
	void OnCollisionEnter2D(Collision2D coll)
	{
		Destroy (coll.gameObject);
		print (coll.gameObject.transform.name);
	}
}
