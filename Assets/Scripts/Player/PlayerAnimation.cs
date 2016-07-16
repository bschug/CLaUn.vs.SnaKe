using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(Player))]
class PlayerAnimation : MonoBehaviour
{
	public GameObject DustParticle;

	Animator Animator;
	Player Player;
	PlayerMovement PlayerMovement;
	PlayerHealth PlayerHealth;

	int ANIPROP_MovementX;
	int ANIPROP_MovementY;
	int ANIPROP_IsAlive;
	int ANIPROP_IsStunned;

	void Awake () {
		Animator = GetComponent<Animator>();
		Player = GetComponent<Player>();
		PlayerMovement = GetComponent<PlayerMovement>();
		PlayerHealth = GetComponent<PlayerHealth>();
	}

	void Update() {
		// Dust particle is based on actual movement
		DustParticle.SetActive( IsMovingQuickly );

		// animation must be based on input, not actual velocity 
		// because it should show where the clown is trying to move, 
		// which is not necessarily where it's actually moving (e.g. on ice)
		var movement = InputManager.Instance.GetMovement( ClownId );
		Animator.SetFloat( "MovementX", movement.x );
		Animator.SetFloat( "MovementY", movement.y );

		Animator.SetBool( "IsAlive", PlayerHealth.IsAlive );
		Animator.SetBool( "IsStunned", PlayerMovement.IsStunned );
	}

	bool IsMovingQuickly { get { return PlayerMovement.CurrentVelocity.sqrMagnitude > 0.02f; } }
	ClownId ClownId { get { return Player.ClownId; } }
}
