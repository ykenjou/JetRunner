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

	public static StageController GetController() {
		return GameObject.FindGameObjectWithTag ("StageController").GetComponent<StageController>();
	}

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		//cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGround(){
		
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
