using UnityEngine;
using System.Collections;

public class DestroyBombTag : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("destroyBombTag", 1.3f);
	}

	public void destroyBombTag(){
		Destroy (gameObject);
	}
}
