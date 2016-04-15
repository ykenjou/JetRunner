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
	[System.NonSerialized]
	public float gravityScale;
	float jumpStartInterval;
	[System.NonSerialized]
	public float jumpedTime;
	[System.NonSerialized]
	public string JumpBtnState;
	[System.NonSerialized]
	public bool jumpBegan;
	bool damageBool;
	[System.NonSerialized]
	public int life;
	[System.NonSerialized]
	public float score;
	[System.NonSerialized]
	public float oldScore;
	[System.NonSerialized]
	public float addScore;
	[System.NonSerialized]
	public int scoreInt;
	[System.NonSerialized]
	public int oldScoreInt;
	[System.NonSerialized]
	public int stageLevel;
	[System.NonSerialized]
	public int oldStageLevel;
	[System.NonSerialized]
	public bool stageLevelUpBool;
	public Text levelText;

	int coins;
	int oldCoins;
	public Text coinText;

	public Transform startPoint;
	public Text scoreText;

	public LifePanelController lifePanelController;

	public GameObject beamObject;

	Vector3 beamPosition;
	bool shotBool;
	public Button beamButton;

	bool timeScaleSlowBool;

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

		if(timeScaleSlowBool){
			Time.timeScale = 0.3f;
		} else {
			Time.timeScale = 1.0f;
		}

		if(!gameController.gameOverBool && gameController.gameStartBool){
			score =  transform.position.x - startPoint.position.x;
			scoreInt = (int)(score);

			if(scoreInt != oldScoreInt){
				//scoreText.text = (score + addScore).ToString("f1");
				//scoreInt = (int)(score + addScore);
				scoreText.text = scoreInt.ToString();
				oldScoreInt = scoreInt;
			}


			if (scoreInt % 300 == 0 && scoreInt != 0)
			{
				if(stageLevelUpBool){
					stageLevel++;
					levelText.text = stageLevel.ToString();
					stageLevelUpBool = false;
				}
			} else {
				if(!stageLevelUpBool){
					stageLevelUpBool = true;
				}
			}

			if(coins != oldCoins){
				coinText.text = coins.ToString();
				oldCoins = coins;
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



			/*
			if(fuel >= 30){
				beamButton.interactable = true;
			} else {
				beamButton.interactable = false;
			}
			*/

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
		fuel = 100;
		fuelSlider.value = fuel / 100;
		jumpedTime = 0;
		jumpBegan = false;
		gravityScale = 4.0f;
		addSpeed = 0;
		score = 0;
		oldScore = 0;
		score = 0;
		addScore = 0;
		scoreInt = 0;
		oldScoreInt = 0;
		scoreText.text = scoreInt.ToString();
		life = 3;
		lifePanelController.UpdateLife(life);
		stageLevel = 1;
		stageLevelUpBool = true;
		levelText.text = stageLevel.ToString();
		coins = 0;
		oldCoins = 0;
		coinText.text = "0";
		timeScaleSlowBool = false;
	}

	public void JumpBegan(){
		// タッチ開始
		if(playerState == "ground"){
			//playerBody2D.velocity = new Vector2(0.0f,30.0f);
			playerBody2D.AddForce(new Vector2(0,1600));
			playerState = "jump";
		}
	}

	public void JumpStationary(){
		jumpedTime += Time.deltaTime;
		if(jumpStartInterval < jumpedTime){
			if(fuel > 1){
				playerBody2D.velocity = new Vector2(0.0f,6.0f);
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

	void TreadEnemy(){
		fuel += 20;
		if(fuel > 100){
			fuel = 100;
		}
		addScore += 20;
		playerBody2D.AddForce(new Vector2(0,100));
	}

	IEnumerator PlayerDamagedStream(){
		damageBool = false;
		timeScaleSlowBool = true;
		life--;
		playerBody2D.AddForce(new Vector2(-3000,200));
		//fuel -= 30;
		playerState = "dameged";
		yield return new WaitForSeconds(0.05f);
		timeScaleSlowBool = false;
		yield return new WaitForSeconds(1f);
		playerState = "normal";
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

		if(col.gameObject.tag == "DamageObject"){
			TreadEnemy();
			playerState = "ground";
		}

	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "DamageObject"){
			if(damageBool){
				PlayerDamaged();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Coin"){
			coins++;
		}
	}


}
