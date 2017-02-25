using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HalfFruit : MonoBehaviour {

	public int type = 0;
	public Sprite[] fruitSprites;
	private Image image;
	public GameObject particleObj;
	
	void Awake(){
		image = GetComponent<Image> ();	
	}
	public void setType(int type){
		this.type = type;
		image.sprite = fruitSprites [type];
		image.SetNativeSize ();
		
		if (type == Contant.Type_Boom) {
			particleObj.SetActive (true);
		} else 
		{
			particleObj.SetActive (false);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.tag == "Bound") {
			if(type != Contant.Type_Apple && type != Contant.Type_Banana && type != Contant.Type_Basaha  && type != Contant.Type_Peach && type != Contant.Type_Sandia && type != Contant.Type_Boom){
				Destroy (gameObject);
			}
		}
		
	}
}
