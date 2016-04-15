using UnityEngine;
using System.Collections;

public class RollingObjController : MonoBehaviour {

	GameObject player;
	bool leftMove;
	bool stopBool;
	float speed;
	Rigidbody2D rigidBody;
	Vector2 leftMoveVector;
	BoxCollider2D boxCollider;
	CircleCollider2D circleCollider;
	float distance;
	float abDistance;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		speed = Random.Range(3f,5f);
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		leftMoveVector = new Vector2(-speed,0);
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
		circleCollider = gameObject.GetComponent<CircleCollider2D>();
		leftMove = false;
		stopBool = false;
	}
	
	// Update is called once per frame
	void Update () {
		//distance = player.transform.position.x - transform.position.x;
		abDistance = Mathf.Abs(player.transform.position.x - transform.position.x);
		if(abDistance < 7 && !leftMove){
			leftMove = true;
		}
		if(leftMove && !stopBool){
			rigidBody.velocity = leftMoveVector;
		}
	}

	void OnTriggerStay2D(Collider2D col){

		if(col.gameObject.tag == "Player" || col.gameObject.tag =="PlayerAttack"){
			stopBool = true;
			boxCollider.enabled = false;
			circleCollider.enabled = false;
		}

		if(col.gameObject.tag =="DamageObject"){
			Destroy(gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag =="Ground"){
			stopBool = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			stopBool = true;
			boxCollider.enabled = false;
			circleCollider.enabled = false;

		}

		if(col.gameObject.tag =="DamageObject"){
			Destroy(gameObject);
		}
	}


	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
