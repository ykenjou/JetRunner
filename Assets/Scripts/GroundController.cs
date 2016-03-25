using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	public GameObject groundPrefab;
	public Camera mainCamera;
	Vector3 cameraRight;
	float xRandam;
	float yRandam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		
	}

	void OnTriggerExit2D(Collider2D col){
		/*
		if(col.gameObject.tag == "Ground"){
			cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f,1.0f,0.0f));

			xRandam = Random.Range(2.0f,6.0f);
			yRandam = Random.Range(8.0f,11.0f);
			cameraRight.x += xRandam;
			cameraRight.y -= yRandam;
			cameraRight.z = 1;
			Instantiate(groundPrefab,cameraRight,Quaternion.identity);
			//Debug.Log("Ground");
		}
		*/
	}
}
