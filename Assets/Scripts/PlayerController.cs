using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static PlayerController GetController() {
		return GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}

	GameController gameController;

	string playerState;
	Rigidbody2D playerBody2D;

	[System.NonSerialized]
	public float fuel;
	float oldFuel;
	Slider fuelSlider;

	[System.NonSerialized]
	public float addSpeed;

	public float gravityScale;
	float jumpStartInterval;
	public float jumpedTime;
	public string JumpBtnState;
	public bool jumpBegan;
	bool damageBool;

	public int life;

	public float score;
	public float oldScore;

	public Transform startPoint;
	public Text scoreText;

	public LifePanelController lifePanelController;

	public GameObject beamObject;

	Vector3 beamPosition;
	bool shotBool;
	public Button beamButton;

	// Use this for initialization
	void Start () {
		shotBool = true;
		gameController = GameController.GetController();
		playerBody2D = GetComponent<Rigidbody2D>();
		gravityScale = 4.0f;
		jumpStartInterval = 0.2f;
		jumpedTime = 0;
		jumpBegan = false;
		life = 3;
		fuelSlider = GameObject.FindGameObjectWithTag("FuelSlider").GetComponent<Slider>();
		damageBool = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameController.gameOverBool && gameController.gameStartBool){
			score =  transform.position.x - startPoint.position.x;

			if(score != oldScore){
				scoreText.text = score.ToString("f1");
				oldScore = score;
			}

			lifePanelController.UpdateLife(life);

			if(life <= 0){
				gameController.GameOver();
			}

			if(playerState == "jet"){
				if(fuel >= 0){
					fuel -= 80 * Time.deltaTime;
					if(fuel < 0){
						fuel = 0;
					}
				}
			}

			if(playerState == "ground"){
				if(fuel <= 100){
					fuel += 30 * Time.deltaTime;

					if(fuel > 100){
						fuel = 100;
					}

					if(fuel < 0){
						fuel = 0;
					}
				}
			}

			fuelSlider.value = fuel / 100;

			if(fuel >= 30){
				beamButton.interactable = true;
			} else {
				beamButton.interactable = false;
			}

			//fuelText.text = fuel.ToString("f1");

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

		}


		//Debug.Log(playerState);
	}

	void FixedUpdate(){
		if(!gameController.gameOverBool && gameController.gameStartBool){
			playerBody2D.velocity = new Vector2(10.0f+addSpeed ,playerBody2D.velocity.y);
		} else {
			playerBody2D.velocity = new Vector2(0,0);
		}
	}

	public void PlayerReset(){
		transform.position = startPoint.position;
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

	public void BeamShot(){
		if(shotBool && fuel >= 30){
			beamPosition = new Vector3(transform.position.x + 7.8f,transform.position.y,transform.position.z);
			Instantiate(beamObject,beamPosition,Quaternion.identity);
			fuel -= 30;
			shotBool = false;
			Invoke("BeamCharge",1);
		}
	}

	void BeamCharge(){
		shotBool = true;
	}

	void PlayerDamaged(){
		StartCoroutine("PlayerDamagedStream");
	}

	IEnumerator PlayerDamagedStream(){
		damageBool = false;
		life--;
		playerBody2D.AddForce(new Vector2(-3000,200));
		fuel -= 30;
		playerState = "dameged";
		yield return new WaitForSeconds(0.3f);
		playerState = "noraml";
		damageBool = true;
	}

	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Missile"){
			playerState = "ground";
		}
		if(col.gameObject.tag == "GameOver"){
			if(!gameController.gameOverBool){
				gameController.GameOver();
			}
		}

	}

	void OnCollisionEnter2D(Collision2D col){


	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "DamageObject"){
			if(damageBool){
				PlayerDamaged();
			}
		}
	}


}
