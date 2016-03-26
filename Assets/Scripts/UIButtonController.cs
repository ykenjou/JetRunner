using UnityEngine;
using System.Collections;

public class UIButtonController : MonoBehaviour {

	PlayerController playerController;

	void Awake(){
		playerController = PlayerController.GetController();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DownRightBtn(){
		playerController.addSpeed = 3.0f;
	}

	public void UpRightBtn(){
		playerController.addSpeed = 0.0f;
	}

	public void DownLeftBtn(){
		playerController.addSpeed = -3.0f;
	}

	public void UpLeftBtn(){
		playerController.addSpeed = 0.0f;
	}

	public void EnterJumpBtn(){
		playerController.JumpBtnState = "downJumpBtn";
	}

	public void DownJumpBtn(){
		playerController.JumpBtnState = "downJumpBtn";
	}

	public void UpJumpBtn(){
		playerController.JumpBtnState = "upJumpBtn";
	}

	public void ExitJumpBtn(){
		playerController.JumpBtnState = "upJumpBtn";
	}
}
