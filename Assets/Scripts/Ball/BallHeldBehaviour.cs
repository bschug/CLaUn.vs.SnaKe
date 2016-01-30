using UnityEngine;
using System.Collections;

public class BallHeldBehaviour : MonoBehaviour {

	public PlayerBallInteraction CurrentClown;
	Rigidbody2D Rigidbody;

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	void OnEnable() {
		Rigidbody.isKinematic = true;
	}

	void Update () {
		transform.position = CurrentClown.transform.position;
	}
}
