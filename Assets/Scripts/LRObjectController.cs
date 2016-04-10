using UnityEngine;
using System.Collections;

public class LRObjectController : MonoBehaviour {

	public bool leftMove;
	bool playerTread;
	float speed;
	Rigidbody2D rigidBody;
	Vector2 rightMoveVector;
	Vector2 leftMoveVector;
	int lrInt;

	BoxCollider2D[] boxColliders;

	// Use this for initialization
	void Start () {
		lrInt = Random.Range(1,11);
		if(lrInt < 6){
			leftMove = true;
		} else {
			leftMove = false;
		}
		speed = Random.Range(2.5f,4f);
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		rightMoveVector = new Vector2(speed,0);
		leftMoveVector = new Vector2(-speed,0);
		boxColliders = gameObject.GetComponents<BoxCollider2D>();
		playerTread = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!playerTread){
			if(leftMove){
				rigidBody.velocity = leftMoveVector;
			} 

			if(!leftMove){
				rigidBody.velocity = rightMoveVector;
			}
		} else {
			//rigidBody.velocity = new Vector2(0,0);
		}
		
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "LeftCollision"){
			leftMove = false;
		}

		if(col.gameObject.tag == "RightCollision"){
			leftMove = true;
		}

		if(col.gameObject.tag == "Player" || col.gameObject.tag =="PlayerAttack"){
			Destroy(gameObject);
		}

		if(col.gameObject.tag =="DamageObject"){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			boxColliders[0].enabled = false;
			boxColliders[1].enabled = false;
			playerTread = true;
		}

		if(col.gameObject.tag =="DamageObject"){
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
