using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerMovement : MonoBehaviour
{
	Player Player;
	Rigidbody2D Rigidbody2D;

	float StunDurationRemaining;
	public bool IsStunned { get { return StunDurationRemaining > 0; } }

	public float MaxSpeed { get { return BalanceValues.Instance.PlayerSpeed; } }
	public Vector2 CurrentVelocity;

	void Awake() {
		Player = GetComponent<Player>();
		Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		StunDurationRemaining -= Time.deltaTime;

		if (!IsStunned && Intro.Instance.TutorialComplete) {
			UpdateVelocity();
		}
    }

	void UpdateVelocity() {
		var movementInput = InputManager.Instance.GetMovement( Player.ClownId );
		CurrentVelocity = movementInput * MaxSpeed;
		Rigidbody2D.velocity = CurrentVelocity;
	}

	public void Stun() {
		StunDurationRemaining = BalanceValues.Instance.StunDuration;
	}

}
