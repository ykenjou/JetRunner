using UnityEngine;
using System.Collections;

public class JumpEnemyController : MonoBehaviour {

	float jumpInterval;
	float passTime;
	Rigidbody2D rigidBody;
	float jumpForce;
	BoxCollider2D[] boxColliders;
	bool playerTread;

	// Use this for initialization
	void Start () {
		jumpInterval = 1.5f;
		passTime = 0;
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		boxColliders = gameObject.GetComponents<BoxCollider2D>();
		jumpForce = Random.Range(800,1400);
		playerTread = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!playerTread){
			passTime += Time.deltaTime;
			if(passTime > jumpInterval){
				passTime = 0;
				rigidBody.AddForce(new Vector2(0,jumpForce));
			}
		}
	}

	void OnTriggerStay2D(Collider2D col){

		if(col.gameObject.tag == "Player" || col.gameObject.tag =="PlayerAttack"){
			playerTread = true;
			boxColliders[0].enabled = false;
			boxColliders[1].enabled = false;
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
