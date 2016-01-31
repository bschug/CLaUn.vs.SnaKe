using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int Health { get; private set; }
	public bool IsStunned { get; private set; }
	public bool IsAlive { get { return Health > 0; } }

	Rigidbody2D Rigidbody;

	void Awake() {
		Health = BalanceValues.Instance.PlayerHealth;
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (!IsAlive) {
			return;
		}

		var contact = collision.contacts[0];
		if (contact.collider.GetComponent<PlayerDamageSource>() == null) {
			return;
		}

		var ownPosition = (Vector2)transform.position;
		Rigidbody.MovePosition( ownPosition + (ownPosition - contact.point) );
		Rigidbody.velocity = Vector2.zero;

		if (!IsStunned) {
			TakeDamage();
		}
	}

	public void TakeDamage () {
		Health--;
		IsStunned = true;

		if (!IsAlive) {
			LoseCondition.Instance.PlayerDead();
		}
		else {
			Invoke( "RemoveStun", BalanceValues.Instance.StunDuration );
		}
	}

	void RemoveStun() {
		if (IsAlive) {
			IsStunned = false;
		}
	}
}
