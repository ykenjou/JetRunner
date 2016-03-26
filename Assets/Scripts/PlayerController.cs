using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static PlayerController GetController() {
		return GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}
	
	public Text fuelText;
	string playerState;
	Rigidbody2D playerBody2D;
	float fuel = 100;
	[System.NonSerialized]
	public float addSpeed;
	float gravityScale;
	float jumpStartInterval;
	float jumpedTime;
	public string JumpBtnState;
	bool jumpBegan;

	// Use this for initialization
	void Start () {
		playerBody2D = GetComponent<Rigidbody2D>();
		gravityScale = 4.0f;
		jumpStartInterval = 0.2f;
		jumpedTime = 0;
		jumpBegan = false;
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
		} 
		if ( Input.GetKey(KeyCode.LeftArrow) ){
			addSpeed = -3.0f;
		}

		if ( Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) ){
			addSpeed = 0.0f;
		}

		if (Input.GetKeyDown(KeyCode.Space)){
			JumpBegan();
		}

		if (Input.GetKey(KeyCode.Space)){
			JumpStationary();
		}

		if (Input.GetKeyUp(KeyCode.Space)){
			JumpEnded();
		}
		/*
		TouchInfo info = AppUtil.GetTouch();
		if (info == TouchInfo.Began)
		{
			// タッチ開始
			JumpBegan();
		}

		if (info == TouchInfo.Stationary)
		{
			JumpStationary();
		}

		if (info == TouchInfo.Ended)
		{
			JumpEnded();
		}
		*/

		if(JumpBtnState == "downJumpBtn" && jumpBegan == false){
			jumpBegan = true;
			JumpBegan();
		}

		if(JumpBtnState == "downJumpBtn" && jumpBegan == true){
			JumpStationary();
		}

		if(JumpBtnState == "upJumpBtn"){
			jumpBegan = false;
			JumpEnded();
		}

		if(playerState == "normal"){
			jumpBegan = false;
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

	public void JumpBegan(){
		// タッチ開始
		if(playerState == "ground"){
			//playerBody2D.velocity = new Vector2(0.0f,30.0f);
			playerBody2D.AddForce(new Vector2(0,900));
			playerState = "jump";
		}
	}

	public void JumpStationary(){
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

	public void JumpEnded(){
		playerBody2D.gravityScale = gravityScale;
		playerState = "normal";
		jumpedTime = 0;
	}


}
