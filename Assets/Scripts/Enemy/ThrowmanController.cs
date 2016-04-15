using UnityEngine;
using System.Collections;

public class ThrowmanController : MonoBehaviour {
	
	float throwInterval;
	float passTime;
	bool playerTread;
	public GameObject ball;
	BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
		throwInterval = 1.5f;
		passTime = 0;
		playerTread = false;
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!playerTread){
			passTime += Time.deltaTime;
			if(passTime > throwInterval){
				passTime = 0;
				ThrowBall();
			}
		}
	}

	void ThrowBall(){
		Vector3 ballPosition = new Vector3(transform.position.x -0.5f,transform.position.y + 0.5f,transform.position.z);
		Instantiate(ball,ballPosition,Quaternion.identity);
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			boxCollider.enabled = false;
			playerTread = true;
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
