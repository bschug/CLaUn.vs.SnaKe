using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBallInteraction : MonoBehaviour
{
	public ClownId ClownId;
	
	PlayerMovement PlayerMovement;

	bool IsHoldingBall = false;
	float DistanceToBall { get { return Vector2.Distance( Ball.Instance.transform.position, transform.position ); } }
	bool IsBallInJuggleRange { get { return DistanceToBall <= BalanceValues.Instance.JuggleDistance; } }

	void Awake() {
		PlayerMovement = GetComponent<PlayerMovement>();
	}

	public void CatchBall() {
		if (!PlayerMovement.IsStunned) {
			IsHoldingBall = true;
			Ball.Instance.Catch( ClownId );
		}
	}

	void Update() {
		if (PlayerMovement.IsStunned) {
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
