using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerBallInteraction : MonoBehaviour
{
	public ClownId ClownId;

	bool IsHoldingBall = false;
	float DistanceToBall { get { return Vector2.Distance( Ball.Instance.transform.position, transform.position ); } }
	bool IsBallInJuggleRange { get { return DistanceToBall <= BalanceValues.Instance.JuggleDistance; } }

	public void CatchBall() {
		IsHoldingBall = true;
	}

	void Update() {
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
	}
}
