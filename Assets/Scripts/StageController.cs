using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {

	Vector3 cameraRight;
	Vector3 playerPosition;
	GameObject mainCamera;
	GameObject[] grounds;
	GameObject[] damageObjects;
	public GameObject groundPrefab;
	public GameObject ground2Prefab;
	public GameObject ground3Prefab;
	public GameObject startGround;
	public GameObject player;
	float xRandam;
	float yRandam;
	//bool nextInit = false;
	public GameObject bombPrefab;
	public GameObject gardRobotPrefab;
	float newGroundWidth;
	float bombWidth;
	float bombHeight;
	float gardRobotWidth;
	float gardRobotHeight;
	float groundRandom;
	float enemyRandom;
	GameObject groundObj;

	PlayerController playerController;

	public static StageController GetController() {
		return GameObject.FindGameObjectWithTag ("StageController").GetComponent<StageController>();
	}

	// Use this for initialization
	void Start () {
		playerController = PlayerController.GetController();

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		bombWidth =  bombPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		bombHeight =  bombPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
		gardRobotWidth =  gardRobotPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		gardRobotHeight =  gardRobotPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
		//cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));
	}

	public void SetNextGround(float oldGroundWidth){
		cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));
		xRandam = Random.Range(oldGroundWidth + 5.0f, oldGroundWidth + 7.0f);
		yRandam = Random.Range(6.0f,12.0f);
		cameraRight.x += xRandam;
		cameraRight.y -= yRandam;
		cameraRight.z = 1;

		groundRandom = Random.Range(0,10.0f);
		if(groundRandom < 3.3){
			groundObj = Instantiate(groundPrefab,cameraRight,Quaternion.identity) as GameObject;
		} else if(groundRandom < 6.6){
			groundObj = Instantiate(ground2Prefab,cameraRight,Quaternion.identity) as GameObject;
		} else {
			groundObj = Instantiate(ground3Prefab,cameraRight,Quaternion.identity) as GameObject;
		}
		groundObj.name = groundPrefab.name;

		newGroundWidth = groundObj.GetComponent<SpriteRenderer>().bounds.size.x;

		enemyRandom = Random.Range(0,10.0f);
		if(enemyRandom < 3){
			//set bomb
			float rPositionX = Random.Range(3,3);
			Vector3 enemyPosition = new Vector3(cameraRight.x + newGroundWidth / 2 - bombWidth /2 + rPositionX,cameraRight.y + bombHeight, cameraRight.z);
			Instantiate(bombPrefab,enemyPosition,Quaternion.identity);
		} else if(enemyRandom < 6){
			//set robot

			float rPositionX = Random.Range(0,3);
			Vector3 enemyPosition = new Vector3(cameraRight.x + newGroundWidth / 2 - gardRobotWidth /2 + rPositionX,cameraRight.y + gardRobotHeight, cameraRight.z);
			Instantiate(gardRobotPrefab,enemyPosition,Quaternion.identity);


		}
	}

	public void SetFirstGround(){
		cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));
		xRandam = Random.Range(3.0f,5.0f);
		yRandam = Random.Range(6.0f,12.0f);
		cameraRight.x += xRandam;
		cameraRight.y -= yRandam;
		cameraRight.z = 1;
		var groundObj = Instantiate(groundPrefab,cameraRight,Quaternion.identity);
		groundObj.name = groundPrefab.name;

		playerPosition = new Vector3(player.transform.position.x-8,player.transform.position.y-0.7f,player.transform.position.z);
		Instantiate(startGround,playerPosition,Quaternion.identity);
	}

	public void DeleteAllGround(){
		grounds = GameObject.FindGameObjectsWithTag("Ground");
		foreach(GameObject obj in grounds){
			Destroy(obj);
		}
	}

	public void DeleteAllObjects(){
		damageObjects = GameObject.FindGameObjectsWithTag("DamageObject");
		if(damageObjects.Length > 0){
			foreach(GameObject obj in damageObjects){
				Destroy(obj);
			}
		}
	}
}
