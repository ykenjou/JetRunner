using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public Text fuelText;
	string playerState;
	Rigidbody2D playerBody2D;
	float fuel = 100;
	float addSpeed;
	float gravityScale;
	float jumpStartInterval;
	float jumpedTime;

	// Use this for initialization
	void Start () {
		playerBody2D = GetComponent<Rigidbody2D>();
		gravityScale = 4.0f;
		jumpStartInterval = 0.15f;
		jumpedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(playerState == "jet"){
			if(fuel >= 0){
				fuel -= 80 * Time.deltaTime;
			}
		}

		if(playerState == "ground"){
			if(fuel <= 100){
				fuel += 30 * Time.deltaTime;
			}
		}

		fuelText.text = fuel.ToString("f1");

		if ( Input.GetKey(KeyCode.RightArrow) ){
			addSpeed = 3.0f;
		} else if ( Input.GetKey(KeyCode.LeftArrow) ){
			addSpeed = -3.0f;
		} else {
			addSpeed = 0.0f;
		}

		TouchInfo info = AppUtil.GetTouch();
		if (info == TouchInfo.Began)
		{
			// タッチ開始
			if(playerState == "ground"){
				//playerBody2D.velocity = new Vector2(0.0f,30.0f);
				playerBody2D.AddForce(new Vector2(0,900));
				playerState = "jump";

			}
		}

		if (info == TouchInfo.Stationary)
		{
			jumpedTime += Time.deltaTime;
			if(jumpStartInterval < jumpedTime){
				if(fuel > 1){
					playerBody2D.velocity = new Vector2(0.0f,5.0f);
					playerBody2D.gravityScale = 0.0f;
					playerState = "jet";
				} else {
					playerBody2D.gravityScale = gravityScale;
				}
			}

		}

		if (info == TouchInfo.Ended)
		{
			//playerBody2D.velocity = new Vector2(0.0f,0.0f);
			playerBody2D.gravityScale = gravityScale;
			playerState = "normal";
			jumpedTime = 0;
		}


		//Debug.Log(playerState);
	}

	void FixedUpdate(){
		playerBody2D.velocity = new Vector2(8.0f+addSpeed ,playerBody2D.velocity.y);
	}

	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.tag == "Ground"){
			playerState = "ground";
		}
			
	}


}
