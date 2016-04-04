using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	Vector2 speed;
	Vector2 transformPosition;
	Vector3 cameraLeft;
	GameObject mainCamera;

	// Use this for initialization
	void Start () {
		speed = new Vector2(-0.1f,0);
		transformPosition = transform.position;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
		transformPosition.x += speed.x;

		transform.position = transformPosition;

		cameraLeft = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(-0.2f,1.0f,0.0f));

		if(transform.position.x < cameraLeft.x){
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerAttack"){
			Destroy(gameObject);
		}
	}
}
