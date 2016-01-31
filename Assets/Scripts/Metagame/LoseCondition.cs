using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseCondition : SingletonMonoBehaviour<LoseCondition> {

	public GameObject LoseScreen;
	public float RestartDelay;
	public SuddenDeath SuddenDeath;

	int NumPlayersDead = 0;

	public void PlayerDead() {
		NumPlayersDead++;

		if (NumPlayersDead == 2) {
			SoundManager.Instance.Cheer();
			LoseScreen.SetActive( true );
			Invoke( "RestartScene", RestartDelay );
		}
		else if (NumPlayersDead == 1) {
			SoundManager.Instance.Boo();
			SuddenDeath.enabled = true;
		}
	}

	void RestartScene() {
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}
}
