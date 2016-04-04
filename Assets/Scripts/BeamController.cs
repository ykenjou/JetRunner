using UnityEngine;
using System.Collections;

public class BeamController : MonoBehaviour {

	float destroyInterval;
	float existTime;

	// Use this for initialization
	void Start () {
		destroyInterval = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {
		existTime += Time.deltaTime;
		if(existTime > destroyInterval){
			DestroySelf();
		}
	}

	void DestroySelf(){
		Destroy(gameObject);
	}
}
