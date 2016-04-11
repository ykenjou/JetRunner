using UnityEngine;
using System.Collections;

public class GroundColController : MonoBehaviour {
	//Transform playerTrans;
	BoxCollider2D boxCollider;
	BoxCollider2D[] boxColliders;
	GameObject mainCamera;
	bool nextInit = false;
	//bool setOff;
	Vector3 cameraRight;
	Vector3 cameraLeft;
	public GameObject groundPrefab;
	public GameObject groundPrefab2;
	public GameObject groundPrefab3;
	public GameObject bombPrefab;
	public GameObject gardRobotPrefab;
	float xRandam;
	float yRandam;
	float groundWidth;
	/*
	float bombWidth;
	float bombHeight;
	float gardRobotWidth;
	float gardRobotHeight;
	*/
	float groundRandom;
	float enemyRandom;
	StageController stageController;

	// Use this for initialization
	void Start () {
		//playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		//boxCollider = gameObject.GetComponent<BoxCollider2D>();
		groundWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
		/*
		bombWidth =  bombPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		bombHeight =  bombPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
		gardRobotWidth =  gardRobotPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		gardRobotHeight =  gardRobotPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
		*/
		stageController = StageController.GetController();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		float f = playerTrans.position.y - boxCollider.bounds.max.y;

		if(f <= 0){
			boxCollider.enabled = false;
		} else {
			boxCollider.enabled = true;
		}
		*/

		/*
		if(setOff){
			boxCollider.enabled = false;
		}
		if(!setOff){
			boxCollider.enabled = true;
		}
		*/


		cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));
		//cameraLeft = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(-1.0f,1.0f,0.0f));
		//Debug.Log(transform.position.x);
		//Debug.Log(cameraRight.x);
		//cameraInitPoint = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));


		if(transform.position.x < cameraRight.x){
			if(!nextInit){
				nextInit = true;
				//Debug.Log("nextInit");
				/*
				xRandam = Random.Range(groundWidth + 5.0f, groundWidth + 7.0f);
				yRandam = Random.Range(6.0f,12.0f);
				cameraRight.x += xRandam;
				cameraRight.y -= yRandam;
				cameraRight.z = 1;
				*/
				stageController.SetNextGround(groundWidth);


				/*
				var groundObj = Instantiate(groundPrefab,cameraRight,Quaternion.identity);
				groundObj.name = groundPrefab.name;


				enemyRandom = Random.Range(0,10.0f);
				if(enemyRandom < 4){
					//set bomb
					float rPositionX = Random.Range(0,3);
					Vector3 enemyPosition = new Vector3(cameraRight.x + groundWidth / 2 - bombWidth /2 + rPositionX,cameraRight.y + bombHeight, cameraRight.z);
					Instantiate(bombPrefab,enemyPosition,Quaternion.identity);
				} else if(enemyRandom < 8){
					//set robot
					float rPositionX = Random.Range(0,3);
					Vector3 enemyPosition = new Vector3(cameraRight.x + groundWidth / 2 - gardRobotWidth /2 + rPositionX,cameraRight.y + gardRobotHeight, cameraRight.z);
					Instantiate(gardRobotPrefab,enemyPosition,Quaternion.identity);

				}
				*/
			}
		}

		/*
		if(transform.position.x < cameraLeft.x){
			Destroy(gameObject);
		}
		*/

	}

	void SetEnemy(){
		
	}


	void OnTriggerStay2D (Collider2D col){
		if (col.gameObject.tag == "Player") {
			//setOff = true;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			//setOff = false;
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}


}
