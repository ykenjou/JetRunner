﻿using UnityEngine;
using System.Collections;

public class LifePanelController : MonoBehaviour {

	public GameObject[] icons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateLife(int life){
		for(int i = 0; i < icons.Length; i++){
			if( i < life){
				icons[i].SetActive(true);
			} else {
				icons[i].SetActive(false);
			}
		}
			
	}
}
