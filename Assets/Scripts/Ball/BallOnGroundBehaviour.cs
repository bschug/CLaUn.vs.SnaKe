using UnityEngine;
using System.Collections;

public class BallOnGroundBehaviour : MonoBehaviour {

	Rigidbody2D Rigidbody;

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	void OnEnable() {
		Rigidbody.velocity = Vector2.zero;
	}
}
