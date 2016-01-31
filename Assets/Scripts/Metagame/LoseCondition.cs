using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseCondition : SingletonMonoBehaviour<LoseCondition> {

	public GameObject LoseScreen;
	public float RestartDelay;

	int NumPlayersDead = 0;

	public void PlayerDead() {
		NumPlayersDead++;

		if (NumPlayersDead == 2) {
			LoseScreen.SetActive( true );
			Invoke( "RestartScene", RestartDelay );
		}
	}

	void RestartScene() {
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}
}
