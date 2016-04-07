using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	Vector2 speed;
	Vector2 transformPosition;

	// Use this for initialization
	void Start () {
		speed = new Vector2(-0.1f,0);
		transformPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transformPosition.x += speed.x;
		transform.position = transformPosition;
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerAttack"){
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
