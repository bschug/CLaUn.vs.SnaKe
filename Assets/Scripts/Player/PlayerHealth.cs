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
		if (IsStunned || !IsAlive) {
			return;
		}

		var contact = collision.contacts[0];
		var segment = contact.collider.GetComponent<SnakeSegment>();
		if (segment != null) {
			Health--;
			IsStunned = true;
			Rigidbody.MovePosition( transform.position + (transform.position - segment.transform.position) );
			Rigidbody.velocity = Vector2.zero;
			Invoke( "RemoveStun", BalanceValues.Instance.StunDuration );

			if (!IsAlive) {
				LoseCondition.Instance.PlayerDead();
			}
		}
	}

	void RemoveStun() {
		if (IsAlive) {
			IsStunned = false;
		}
	}
}
