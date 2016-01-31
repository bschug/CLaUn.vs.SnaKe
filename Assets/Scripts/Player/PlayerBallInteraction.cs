using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerBallInteraction : MonoBehaviour
{
	public ClownId ClownId;

	PlayerHealth PlayerHealth;

	bool IsHoldingBall = false;
	float DistanceToBall { get { return Vector2.Distance( Ball.Instance.transform.position, transform.position ); } }
	bool IsBallInJuggleRange { get { return DistanceToBall <= BalanceValues.Instance.JuggleDistance; } }

	void Awake() {
		PlayerHealth = GetComponent<PlayerHealth>();
	}

	public void CatchBall() {
		if (!PlayerHealth.IsStunned) {
			IsHoldingBall = true;
			Ball.Instance.Catch( ClownId );
		}
	}

	void Update() {
		if (PlayerHealth.IsStunned) {
			return;
		}

		if (InputManager.Instance.WasThrowPressedThisFrame(ClownId)) {
			Throw();
		}
	}

	void Throw() {
		if (IsHoldingBall) {
			Ball.Instance.Throw( ClownId );
		}
		else if (IsBallInJuggleRange) {
			Ball.Instance.Juggle( ClownId );
		}
		IsHoldingBall = false;
	}
}
