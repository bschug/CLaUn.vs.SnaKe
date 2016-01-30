using UnityEngine;
using System.Collections;

public class BallOnGroundBehaviour : MonoBehaviour {

	Rigidbody2D Rigidbody;

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	void OnEnable() {
		Rigidbody.velocity = Vector2.zero;
		Rigidbody.isKinematic = true;
	}

	void Update() {
		if (CheckForPickup( ClownId.Little )) {
			return;
		}
		if (CheckForPickup( ClownId.Big )) {
			return;
		}
	}

	bool CheckForPickup (ClownId clownId) {
		var clown = PlayerRegistry.Instance.GetClown( clownId ).GetComponent<PlayerBallInteraction>();
		var distance = Vector2.Distance( clown.transform.position, transform.position );
		if (distance <= BalanceValues.Instance.PickupDistance) {
			clown.CatchBall();
			return true;
		}
		return false;
	}
}
