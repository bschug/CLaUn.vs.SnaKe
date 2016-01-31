using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intro : SingletonMonoBehaviour<Intro> {

	public GameObject SplashScreen;

	GameObject LittleClown { get { return PlayerRegistry.Instance.LittleClown; } }
	GameObject BigClown { get { return PlayerRegistry.Instance.BigClown; } }

	Dictionary<ClownId, bool> HasThrown = new Dictionary<ClownId, bool>();
	public bool TutorialComplete { get { return HasThrown[ClownId.Little] && HasThrown[ClownId.Big]; } }

	public void BallThrown (ClownId clownId) {
		HasThrown[clownId] = true;
	}

	void Start() {
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
