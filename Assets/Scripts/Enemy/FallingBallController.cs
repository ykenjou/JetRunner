using UnityEngine;
using System.Collections;

public class FallingBallController : MonoBehaviour {

	Rigidbody2D rigidBody;
	GameObject player;
	float abDistance;
	bool setGravityBool;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		setGravityBool = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!setGravityBool){
			abDistance = Mathf.Abs(player.transform.position.x - transform.position.x);
			if(abDistance < 11){
				setGravityBool = true;
				rigidBody.gravityScale = 1;
			}
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
