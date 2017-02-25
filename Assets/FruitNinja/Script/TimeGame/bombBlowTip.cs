using UnityEngine;
using System.Collections;

public class bombBlowTip : MonoBehaviour {
	public RectTransform bombBlowTipRtf;
	void Update(){
		transform.SetAsLastSibling ();
	}
}
