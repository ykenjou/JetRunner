using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	Rigidbody2D cameraBody2D;
	GameObject rightLine;
	GameObject leftLine;

	// Use this for initialization
	void Start () {
		cameraBody2D = GetComponent<Rigidbody2D>();
		//rightLine = GameObject.FindGameObjectWithTag("RightLine");
		//leftLine = GameObject.FindGameObjectWithTag("LeftLine");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);

		cameraBody2D.velocity = new Vector2(8.0f,cameraBody2D.velocity.y);

		//Vector3 cameraFront = gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.02f,1.0f,0.0f));
		//rightLine.transform.position = cameraFront;

		//Vector3 cameraLeft = gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(-0.5f,1.0f,0.0f));
		//leftLine.transform.position = cameraLeft;
	}
}
