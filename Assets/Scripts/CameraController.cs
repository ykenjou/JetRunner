using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	Rigidbody2D cameraBody2D;
	GameObject rightLine;
	GameObject leftLine;

	GameController gameController;

	public GameObject cameraStartPoint;

	public static CameraController GetController() {
		return GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController>();
	}

	// Use this for initialization
	void Start () {
		cameraBody2D = GetComponent<Rigidbody2D>();
		//rightLine = GameObject.FindGameObjectWithTag("RightLine");
		//leftLine = GameObject.FindGameObjectWithTag("LeftLine");
		gameController = GameController.GetController();
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameController.gameOverBool && gameController.gameStartBool){
			transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
			cameraBody2D.velocity = new Vector2(12.0f,cameraBody2D.velocity.y);
		} else {
			cameraBody2D.velocity = new Vector2(0,0);
		}
	}

	void FixedUpdate(){

		//Vector3 cameraFront = gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.02f,1.0f,0.0f));
		//rightLine.transform.position = cameraFront;

		//Vector3 cameraLeft = gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(-0.5f,1.0f,0.0f));
		//leftLine.transform.position = cameraLeft;
	}

	public void ResetCameraPosition(){
		transform.position = cameraStartPoint.transform.position;
	}
}
