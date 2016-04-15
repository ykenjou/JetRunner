using UnityEngine;
using System.Collections;

public class MissleCreateController : MonoBehaviour {

	Vector3 cameraRight;
	public GameObject missilePrefab;
	float missileInterval;
	float passedTime;
	GameObject mainCamera;
	float yRandam;

	GameController gameController;
	PlayerController playerController;

	// Use this for initialization
	void Start () {
		gameController = GameController.GetController();
		playerController = PlayerController.GetController();
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		missileInterval = 8;
		passedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameController.gameOverBool && gameController.gameStartBool && playerController.stageLevel >= 8){
			passedTime += Time.deltaTime;
			if(missileInterval < passedTime){
				passedTime = 0;

				cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.3f,1.0f,0.0f));
				yRandam = Random.Range(3.0f,12.0f);
				cameraRight.y -= yRandam;
				cameraRight.z = 1;
				Instantiate(missilePrefab,cameraRight,Quaternion.identity);
			}
		}

	}
}
