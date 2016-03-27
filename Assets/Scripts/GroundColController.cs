using UnityEngine;
using System.Collections;

public class GroundColController : MonoBehaviour {
	Transform playerTrans;
	BoxCollider2D boxCollider;
	BoxCollider2D[] boxColliders;
	GameObject mainCamera;
	bool nextInit = false;
	Vector3 cameraRight;
	Vector3 cameraLeft;
	Vector3 cameraInitPoint;
	public GameObject groundPrefab;
	float xRandam;
	float yRandam;
	float groundWidth;

	// Use this for initialization
	void Start () {
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
		groundWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		float f = playerTrans.position.y - boxCollider.bounds.max.y;

		if(f <= 0){
			boxCollider.enabled = false;
		} else {
			boxCollider.enabled = true;
		}

		cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));
		cameraLeft = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(-0.2f,1.0f,0.0f));
		//Debug.Log(transform.position.x);
		//Debug.Log(cameraRight.x);
		//cameraInitPoint = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));


		if(transform.position.x < cameraRight.x){
			if(!nextInit){
				nextInit = true;
				//Debug.Log("nextInit");
				xRandam = Random.Range(groundWidth + 2.0f, groundWidth + 5.0f);
				yRandam = Random.Range(6.0f,12.0f);
				cameraRight.x += xRandam;
				cameraRight.y -= yRandam;
				cameraRight.z = 1;
				var groundObj = Instantiate(groundPrefab,cameraRight,Quaternion.identity);
				groundObj.name = groundPrefab.name;
			}
		}

		if(transform.position.x < cameraLeft.x){
			Destroy(gameObject);
		}

	}

}
