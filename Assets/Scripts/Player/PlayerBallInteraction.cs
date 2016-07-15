using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerBallInteraction : MonoBehaviour
{
	Player Player;
	PlayerMovement PlayerMovement;
	
	ClownId ClownId { get { return Player.ClownId; } }

	bool IsHoldingBall = false;
	float DistanceToBall { get { return Vector2.Distance( Ball.Instance.transform.position, transform.position ); } }
	bool IsBallInJuggleRange { get { return DistanceToBall <= BalanceValues.Instance.JuggleDistance; } }

	void Awake() {
		Player = GetComponent<Player>();
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
