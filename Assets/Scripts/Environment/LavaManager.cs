using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaManager : SingletonMonoBehaviour<LavaManager> {

	List<LavaView> LavaViews = new List<LavaView>();

	public int NumLavas = 1;

	public float ShowDuration = 1.5f;
	public float LavaDuration = 2f;
	public float HideDuration = 1f;

	void Start() {
		for (var i = 0; i<NumLavas; i++) {
			StartCoroutine( Co_ManageLava() );
		}
	}

	public void RegisterLava(LavaView lava) {
		LavaViews.Add( lava );
	}

	IEnumerator Co_ManageLava() {
		while (!Intro.Instance.TutorialComplete) {
			yield return null;
		}

		while (true) {
			if (LavaViews.Count == 0) {
				continue;
			}

			var i = Random.Range( 0, LavaViews.Count );
			var lava = LavaViews[i];
			yield return StartCoroutine( lava.Co_ShowLava( ShowDuration ) );
			yield return new WaitForSeconds( LavaDuration );
			yield return StartCoroutine( lava.Co_HideLava( HideDuration ) );
		}
	}
}
