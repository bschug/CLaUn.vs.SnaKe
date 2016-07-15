using UnityEngine;
using System.Collections;
public enum AnimationStates
{
	Idle,
	Walk_Left,
	Walk_Right,
	Walk_Front,
	Walk_Back,
	Stun,
	Death,

	Default
}

[RequireComponent(typeof(PlayerHealth))]
public class PlayerMovement : MonoBehaviour
{
	public GameObject[] walkAnimations;
	public ClownId ClownId;
	public AnimationStates animStates;

	public GameObject cloudParticle;

	PlayerHealth PlayerHealth;

	float StunDurationRemaining;
	public bool IsStunned { get { return StunDurationRemaining > 0; } }

	float Speed { get { return BalanceValues.Instance.PlayerSpeed; } }

	void Awake() {
		PlayerHealth = GetComponent<PlayerHealth>();
	}

	void FixedUpdate() {
		StunDurationRemaining -= Time.deltaTime;
		if (!IsStunned && Intro.Instance.TutorialComplete) {
			var movement = InputManager.Instance.GetMovement( ClownId );
			UpdatePosition( movement );
		}
		PlayerDirAndSetAnimationStates();
		SetAnimation();
    }

	void UpdatePosition (Vector2 movement) {
		GetComponent<Rigidbody2D>().velocity = movement * Speed;
	}

	public void Stun() {
		StunDurationRemaining = BalanceValues.Instance.StunDuration;
	}

	void PlayerDirAndSetAnimationStates() {
		Vector2 playerDir = InputManager.Instance.GetMovement( ClownId );
		
		if (!PlayerHealth.IsAlive) {
			animStates = AnimationStates.Death;
		}
		else if (IsStunned) {
			animStates = AnimationStates.Stun;
		}
		else if( Mathf.Abs(playerDir.x) <= 0.1f && Mathf.Abs(playerDir.y) <= 0.1f) {
			animStates = AnimationStates.Idle;
        }
		else if (playerDir.x <= 0 && playerDir.y == 0) {
			//links
			animStates = AnimationStates.Walk_Left;
		}
		else if (playerDir.x >= 0 && playerDir.y == 0) {
			//rechts
			animStates = AnimationStates.Walk_Right;
		}
		else if (playerDir.x == 0 && playerDir.y >= 0) {
			//oben
			animStates = AnimationStates.Walk_Back;
		}
		else if (playerDir.x == 0 && playerDir.y <= 0) {
			//unten
			animStates = AnimationStates.Walk_Front;
		}
	}

	void SetAnimation() {


		if(animStates == AnimationStates.Idle) {
			walkAnimations[0].SetActive( true );
			cloudParticle.SetActive( false );

		}
		else {
			walkAnimations[0].SetActive( false );
			cloudParticle.SetActive( true );
		}
		if (animStates == AnimationStates.Walk_Left) {
			walkAnimations[1].SetActive( true );
		}
		else {
			walkAnimations[1].SetActive( false );
		}
		if (animStates == AnimationStates.Walk_Right) {
			walkAnimations[2].SetActive( true );
		}
		else {
			walkAnimations[2].SetActive( false );
		}
		if (animStates == AnimationStates.Walk_Front) {
			walkAnimations[3].SetActive( true );
		}
		else {
			walkAnimations[3].SetActive( false );
		}
		if (animStates == AnimationStates.Walk_Back) {
			walkAnimations[4].SetActive( true );
		}
		else {
			walkAnimations[4].SetActive( false );
		}
		if (animStates == AnimationStates.Stun) {
			walkAnimations[5].SetActive( true );
		}
		else {
			walkAnimations[5].SetActive( false );
		}
		if (animStates == AnimationStates.Death) {
			walkAnimations[6].SetActive( true );
		}
		else {
			walkAnimations[6].SetActive( false );
		}

	}



}
