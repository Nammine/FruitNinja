using UnityEngine;
using System.Collections;

public class DetectIfCut : MonoBehaviour {

	public HalfFruit halfFruitClone;
	public Splash splashClone;

	private Game2Rule gameRule;

	public int forceX = 300;
	public int forceHalfY = 3000;
	private const int fruitOutY = -275;

	public bool needDestroy = false;

	public int fruitType;

	public AudioClip slatter;
	AudioSource sound;
	
	void Start () {
		GameObject gamePanelObj = EntityManager.Instance.getEntityFromId (Singleton.gameType);
		gameRule = gamePanelObj.GetComponent<Game2Rule> ();
		sound = gamePanelObj.GetComponent<AudioSource> ();
	}

	void Update(){
		if (needDestroy) {
			if (fruitType != Contant.Type_Boom) {
				createRightLeftFruit ();
				Destroy (gameObject);			
			} else {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Bound") {
			if (fruitType == Contant.Type_Boom) {
				MessageDispatcher.Instance.dispatchMessage (0.0f, Singleton.gameType, MessageType.Msg_Scored, new Vector2(0,0), 0);
			}
			Destroy (gameObject);
		} else if (other.tag == "HandPosCollider") {
			if (other.GetComponent<HandPosVelocity> ().velocity > SingularValue.minDistance) {
				if (fruitType == Contant.Type_Boom) {
					MessageDispatcher.Instance.dispatchMessage(0.0f, Singleton.gameType, MessageType.Msg_CutBomb, getPos (), 0 );
					Destroy (gameObject);
				} else {
					MessageDispatcher.Instance.dispatchMessage (0.0f, Singleton.gameType, MessageType.Msg_Scored, new Vector2(0,0), 0);
					createRightLeftFruit ();
					playMusic ();
					Destroy (gameObject);	
				}
			}
		} else if (other.tag == "Fruit") {
			if(fruitType == Contant.Type_Boom){
				Debug.Log("encounter fruit");
				int forceX = Random.Range (3,10) * 1000;
				int dir = Random.Range (0,1);
				if(dir == 0){
					this.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 0));
				}
				else {
					this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-forceX, 0));
				}
			}
		}

	}

	private Vector2 getPos(){
		RectTransform curRtf = transform as RectTransform;
		return curRtf.anchoredPosition;
	}

	private void playMusic ()
	{
		sound.clip = slatter;
		sound.pitch = 1;
		sound.volume = 1;
		sound.Play ();
	}

	private void createRightLeftFruit ()
	{
		HalfFruit leftFruit = Instantiate (halfFruitClone, transform.position, transform.rotation) as HalfFruit;
		leftFruit.setType (fruitType + 1);
		newFruitInit (leftFruit, true);
		HalfFruit rightFruit = Instantiate (halfFruitClone, transform.position, transform.rotation) as HalfFruit;
		rightFruit.setType (fruitType + 2);
		newFruitInit (rightFruit, false);
		Splash sl = Instantiate (splashClone) as Splash;
		sl.setColor (fruitType);
		newSplashInit (sl);
		Destroy (sl.gameObject, 1f);
	}

	private void newSplashInit (Splash s)
	{
		RectTransform curRtf = transform as RectTransform;
		s.transform.SetParent (gameRule.transform);
		RectTransform rtf = s.transform as RectTransform;
		rtf.anchoredPosition3D = new Vector3 (0, 0, 0);
		rtf.anchoredPosition = curRtf.anchoredPosition;
		rtf.localScale = new Vector3 (1, 1, 1);
	}
	
	private void newFruitInit (HalfFruit fruit, bool isLeft)
	{
		
		RectTransform curRtf = transform as RectTransform;
		fruit.transform.SetParent (gameRule.transform);
		RectTransform rtf = fruit.transform as RectTransform;
		rtf.anchoredPosition3D = new Vector3 (0, 0, 0);
		rtf.anchoredPosition = curRtf.anchoredPosition;
		rtf.localScale = new Vector3 (1, 1, 1);
		
		Rigidbody2D rb2d = fruit.GetComponent<Rigidbody2D> ();
		if (isLeft) {
			rb2d.AddForce (new Vector2 (-forceX, forceHalfY));
		} else {
			rb2d.AddForce (new Vector2 (forceX, forceHalfY));
		}
	}

}
