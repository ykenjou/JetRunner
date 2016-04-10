using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	[System.NonSerialized]
	public bool gameStartBool;
	[System.NonSerialized]
	public bool gameOverBool;

	public GameObject gameOverPanel;
	public GameObject gameStartPanel;
	public GameObject goText;

	PlayerController playerController;
	CameraController cameraController;
	StageController stageController;

	public static GameController GetController() {
		return GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	}

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		playerController = PlayerController.GetController();
		cameraController = CameraController.GetController();
		stageController = StageController.GetController();
		GameReset();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % 30 == 0)
		{
			System.GC.Collect();
		}
	
	}

	public void GameReset(){
		gameOverBool = false;
		gameStartBool = false;
		gameOverPanel.SetActive(false);
		gameStartPanel.SetActive(true);
		goText.SetActive(false);
		playerController.PlayerReset();
		cameraController.ResetCameraPosition();
		stageController.DeleteAllGround();
		stageController.DeleteAllObjects();
		stageController.DeleteCoins();
		stageController.SetFirstGround();
	}

	public void GameStart(){
		StartCoroutine("GameStartStream");
	}

	IEnumerator GameStartStream(){
		gameStartPanel.SetActive(false);
		goText.SetActive(true);
		yield return new WaitForSeconds(1);
		goText.SetActive(false);
		gameStartBool = true;
	}

	public void GameOver(){
		StartCoroutine("GameOverStream");
	}

	IEnumerator GameOverStream(){
		gameOverBool = true;
		yield return new WaitForSeconds(1);
		gameOverPanel.SetActive(true);
	}

	void DestroyObjects(){
		
	}
		
}
