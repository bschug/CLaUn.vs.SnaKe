using UnityEngine;
using System.Collections;

public class RegisterPlayers : MonoBehaviour {

	public GameObject[] littlePlayerState = new GameObject[2];
	public GameObject[] bigPlayerState = new GameObject[2];

	public static RegisterPlayers instance;

	public  bool littleCheck;
	public  bool bigCheck;

	private int elementOne = 1;

	void Awake() {
		instance = this;
	}
	void Update() {

		PlayerSelection();
	}

	void PlayerSelection() {
		if(InputManager.Instance.LittleThrow == XInputDotNetPure.ButtonState.Pressed) {
			bigCheck = true;
			bigPlayerState[elementOne].SetActive( bigCheck );
		}
		else {
			bigCheck = false;
			bigPlayerState[elementOne].SetActive( bigCheck );
		}
		if(InputManager.Instance.BigThrow == XInputDotNetPure.ButtonState.Pressed) {
			littleCheck = true;
			littlePlayerState[elementOne].SetActive( littleCheck );
		}
		else {
			littleCheck = false;
			littlePlayerState[elementOne].SetActive( littleCheck );
		}
	}
}
