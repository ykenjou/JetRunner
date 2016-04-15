using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {

	Vector3 cameraRight;
	Vector3 playerPosition;
	GameObject mainCamera;
	GameObject[] grounds;
	GameObject[] damageObjects;
	GameObject[] coins;
	public GameObject groundPrefab;
	public GameObject ground2Prefab;
	public GameObject ground3Prefab;
	public GameObject ground4Prefab;
	public GameObject ground5Prefab;
	public GameObject ground6Prefab;
	GameObject[] groundPrefabs;
	public GameObject startGround;
	public GameObject player;
	public GameObject Coin;
	float xRandam;
	float yRandam;
	public GameObject bombPrefab;
	public GameObject gardRobotPrefab;
	public GameObject rollingObjPrefab;
	public GameObject jumpEnemyPrefab;
	public GameObject throwmanPrefab;
	public GameObject fallingBallPrefab;
	GameObject[] enemyPrefabs;
	float newGroundWidth;
	float bombWidth;
	float bombHeight;
	float gardRobotWidth;
	float gardRobotHeight;
	float rollingObjWidth;
	float rollingObjHeight;
	float jumpEnemyWidth;
	float jumpEnemyHeight;
	float groundRandom;
	float enemyRandom;
	float itemRandom;
	GameObject groundObj;

	float xRangeMin;
	float xRangeMax;

	float yRangeMin;
	float yRangeMax;

	float levelDivide;

	PlayerController playerController;

	public static StageController GetController() {
		return GameObject.FindGameObjectWithTag ("StageController").GetComponent<StageController>();
	}

	// Use this for initialization
	void Start () {
		playerController = PlayerController.GetController();

		groundPrefabs = new GameObject[10];
		enemyPrefabs = new GameObject[10];

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		bombWidth =  bombPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		bombHeight =  bombPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
		gardRobotWidth =  gardRobotPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		gardRobotHeight =  gardRobotPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
		rollingObjWidth = rollingObjPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		rollingObjHeight = rollingObjPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
		//cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));
	}

	public void SetNextGround(float oldGroundWidth){
		cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));

		RangeSettingWithLevel(playerController.stageLevel);
		xRandam = Random.Range(oldGroundWidth + xRangeMin, oldGroundWidth + xRangeMax);
		yRandam = Random.Range(yRangeMin,yRangeMax);
		cameraRight.x += xRandam;
		cameraRight.y -= yRandam;
		cameraRight.z = 1;

		GroundSettingWithLevel(playerController.stageLevel);

		groundRandom = Random.Range(0,10.0f);
		if(groundRandom < 3.3){
			groundObj = Instantiate(groundPrefabs[0],cameraRight,Quaternion.identity) as GameObject;
		} else if(groundRandom < 6.6){
			groundObj = Instantiate(groundPrefabs[1],cameraRight,Quaternion.identity) as GameObject;
		} else {
			groundObj = Instantiate(groundPrefabs[2],cameraRight,Quaternion.identity) as GameObject;
		}
		groundObj.name = groundPrefab.name;

		newGroundWidth = groundObj.GetComponent<SpriteRenderer>().bounds.size.x;


		enemyRandom = Random.Range(0,10.0f);
		if(enemyRandom < 2.2){
			//set bomb
			float rPositionX = Random.Range(3,3);
			Vector3 enemyPosition = new Vector3(cameraRight.x + newGroundWidth / 2 - bombWidth /2 + rPositionX,cameraRight.y + bombHeight, cameraRight.z);
			Instantiate(bombPrefab,enemyPosition,Quaternion.identity);
		} else if(enemyRandom < 4.4){
			//set robot
			float rPositionX = Random.Range(0,3);
			Vector3 enemyPosition = new Vector3(cameraRight.x + newGroundWidth / 2 - gardRobotWidth /2 + rPositionX,cameraRight.y + gardRobotHeight, cameraRight.z);
			Instantiate(jumpEnemyPrefab,enemyPosition,Quaternion.identity);
		} else if(enemyRandom < 6.6) {
			float rPositionX = Random.Range(3,5);
			Vector3 enemyPosition = new Vector3(cameraRight.x + newGroundWidth / 2 - rollingObjWidth /2 + rPositionX,cameraRight.y + rollingObjHeight, cameraRight.z);
			Instantiate(throwmanPrefab,enemyPosition,Quaternion.identity);
		}

		itemRandom = Random.Range(0,10.0f);
		if(itemRandom < 3){
			float rPositionX = Random.Range(-12,12);
			float rPositonY = Random.Range(3,5);
			Vector3 itemPosition = new Vector3(cameraRight.x + newGroundWidth / 2 + rPositionX,cameraRight.y + rPositonY, cameraRight.z);
			Instantiate(Coin,itemPosition,Quaternion.identity);
		}


	}

	void RangeSettingWithLevel(int level){
		levelDivide = level / 8;
		if(levelDivide > 2){
			levelDivide = 2;
		}
		xRangeMin = 4 + levelDivide;
		xRangeMax = 6 + levelDivide;
		yRangeMin = 6 - levelDivide;
		yRangeMax = 12 + levelDivide;
	}

	void GroundSettingWithLevel(int level){
		if(level <= 2){
			groundPrefabs[0] = groundPrefab;
			groundPrefabs[1] = ground2Prefab;
			groundPrefabs[2] = ground3Prefab;
		} else if(level <= 4){
			groundPrefabs[0] = groundPrefab;
			groundPrefabs[1] = ground3Prefab;
			groundPrefabs[2] = ground4Prefab;
		} else if(level <= 6){
			groundPrefabs[0] = ground2Prefab;
			groundPrefabs[1] = ground4Prefab;
			groundPrefabs[2] = ground5Prefab;
		} else if(level <= 8){
			groundPrefabs[0] = ground3Prefab;
			groundPrefabs[1] = ground4Prefab;
			groundPrefabs[2] = ground5Prefab;
		} else {
			groundPrefabs[0] = ground3Prefab;
			groundPrefabs[1] = ground5Prefab;
			groundPrefabs[2] = ground6Prefab;
		}

	}

	void enemySettingWithLevel(int level){
		if(level <= 1){
			enemyPrefabs[0] = jumpEnemyPrefab;
			enemyPrefabs[1] = jumpEnemyPrefab;
		} else if(level <= 2){
		} else if(level <= 3){
		} else if(level <= 4){
		} else if(level <= 5){
		} else if(level <= 6){
		} else if(level <= 7){
		} else if(level <= 8){
		} else if(level <= 9){
		} else if(level <= 10){
		} else if(level <= 11){
		} else {
			
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

		playerPosition = new Vector3(player.transform.position.x-10,player.transform.position.y-0.7f,player.transform.position.z);
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

	public void DeleteCoins(){
		coins = GameObject.FindGameObjectsWithTag("Coin");
		if(coins.Length > 0){
			foreach(GameObject obj in coins){
				Destroy(obj);
			}
		}
	}
}
