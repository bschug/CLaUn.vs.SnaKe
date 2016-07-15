using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intro : SingletonMonoBehaviour<Intro> {

	public GameObject SplashScreen;

	Dictionary<ClownId, bool> HasThrown = new Dictionary<ClownId, bool>();
	public bool TutorialComplete { get { return IsInitialized && HasThrown[ClownId.Little] && HasThrown[ClownId.Big]; } }
	public bool IsInitialized { get { return HasThrown.ContainsKey( ClownId.Big ) && HasThrown.ContainsKey( ClownId.Little ); } }

	public void BallThrown (ClownId clownId) {
		HasThrown[clownId] = true;
	}

	void Start() {
#if !UNITY_EDITOR
		Cursor.visible = false;
#endif
		StartCoroutine( Co_Intro() );
		HasThrown[ClownId.Little] = false;
		HasThrown[ClownId.Big] = false;
	}

	IEnumerator Co_Intro() {
		yield return StartCoroutine( Co_DropClowns() );
		StartCoroutine( Co_ShowIntroText() );
	}

	IEnumerator Co_DropClowns() {
		yield return null;
	}

	IEnumerator Co_ShowIntroText() {
		SplashScreen.SetActive( true );
		while (!TutorialComplete) {
			yield return null;
		}
		SplashScreen.SetActive( false );
		SnakeFactory.Instance.SpawnSnake();
	}
}
