﻿using UnityEngine;
using System.Collections;

public class SkyController : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		transform.position = new Vector3(player.transform.position.x,transform.position.y,transform.position.z);
	}

}
