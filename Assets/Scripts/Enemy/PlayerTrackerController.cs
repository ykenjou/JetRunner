using UnityEngine;
using System.Collections;

public class PlayerTrackerController : MonoBehaviour {

	GameObject player;
	Rigidbody2D rigidBody;
	bool stopBool;
	bool flipBool;
	float distance;
	float abDistance;
	float speed;
	BoxCollider2D[] boxColliders;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		boxColliders = gameObject.GetComponents<BoxCollider2D>();
		flipBool = false;
		stopBool = false;
		speed = Random.Range(2.5f,5);
	}
	
	// Update is called once per frame
	void Update () {
		if(!stopBool){
			distance = player.transform.position.x - transform.position.x;
			abDistance = Mathf.Abs(player.transform.position.x - transform.position.x);
			if(abDistance < 7){
				//プレイヤーが左 初期
				if(distance < 0){
					rigidBody.velocity = new Vector2(-speed,0);
				} else {
					if(!flipBool){
						flipBool = true;
						Flip();
					}
					rigidBody.velocity = new Vector2(speed,0);
				}
			} else {
				rigidBody.velocity = new Vector2(0,0);
			}
		}
	}

	void Flip(){
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "LeftCollision"){
			stopBool = true;
		}

		if(col.gameObject.tag == "RightCollision"){
			stopBool = true;
		}

		if(col.gameObject.tag == "Player" || col.gameObject.tag =="PlayerAttack"){
			stopBool = true;
			boxColliders[0].enabled = false;
			boxColliders[1].enabled = false;
		}

		if(col.gameObject.tag =="DamageObject"){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			stopBool = true;
			boxColliders[0].enabled = false;
			boxColliders[1].enabled = false;

		}

		if(col.gameObject.tag =="DamageObject"){
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
