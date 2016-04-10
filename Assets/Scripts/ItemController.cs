using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}

}
