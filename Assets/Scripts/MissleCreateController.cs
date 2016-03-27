﻿using UnityEngine;
using System.Collections;

public class MissleCreateController : MonoBehaviour {

	Vector3 cameraRight;
	public GameObject missilePrefab;
	float missileInterval;
	float passedTime;
	GameObject mainCamera;
	float yRandam;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		missileInterval = 5;
		passedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		passedTime += Time.deltaTime;
		if(missileInterval < passedTime){
			passedTime = 0;

			cameraRight = mainCamera.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.3f,1.0f,0.0f));
			yRandam = Random.Range(6.0f,9.0f);
			cameraRight.y -= yRandam;
			Instantiate(missilePrefab,cameraRight,Quaternion.identity);
		}

	}
}