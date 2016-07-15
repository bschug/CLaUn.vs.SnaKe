using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
	public ClownId ClownId;

	void Awake() {
		PlayerRegistry.Instance.RegisterPlayer( this );
	}

	void OnDestroy () {
		if (PlayerRegistry.Instance != null) {
			PlayerRegistry.Instance.RemovePlayer( this );
		}
	}
}
