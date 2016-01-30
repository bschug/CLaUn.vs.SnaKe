using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	public string nextScene;

	// Update is called once per frame
	void Update () {
		GameStart();
	}

	void GameStart () {
		if (RegisterPlayers.instance.littleCheck && RegisterPlayers.instance.bigCheck) {
			// Particle 
			SceneManager.LoadScene( nextScene );
		}
	}
}
