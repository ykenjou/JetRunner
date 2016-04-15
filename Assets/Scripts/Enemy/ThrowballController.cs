using UnityEngine;
using System.Collections;

public class ThrowballController : MonoBehaviour {

	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		rigidBody.AddForce(new Vector2(-600,50));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
