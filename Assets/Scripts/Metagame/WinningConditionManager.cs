using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinningConditionManager : SingletonMonoBehaviour<WinningConditionManager> {
	public GameObject WinScreen;
	public string NextLevelName;

	List<Snake> ActiveSnakes = new List<Snake>();

	public void SnakeSpawned(Snake snake) {
		ActiveSnakes.Add( snake );
	}

	public void SnakeDestroyed(Snake snake) {
		ActiveSnakes.Remove( snake );
		if (ActiveSnakes.Count == 0) {
			WinScreen.SetActive( true );
			Invoke( "LoadNextLevel", BalanceValues.Instance.WinScreenDuration );
		}
	}

	void LoadNextLevel() {
		SceneManager.LoadScene( NextLevelName );
	}
}
