using UnityEngine;
using System.Collections;

public class BallBouncingBehaviour : MonoBehaviour {

	public float MaxTorque;

	Vector2 Direction;
	float HitGroundTime;

	Rigidbody2D Rigidbody;

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	public void Init(Vector2 direction) {
		Direction = direction;
		HitGroundTime = Time.time + BalanceValues.Instance.BallBounceDuration;
		Rigidbody.velocity = Direction * Ball.Instance.Speed;
		Rigidbody.isKinematic = false;
		Rigidbody.AddTorque( Random.Range( -MaxTorque, MaxTorque ) );
		SoundManager.Instance.Swoosh();
	}

	public void FixedUpdate() {
		if (Time.time >= HitGroundTime) {
			Ball.Instance.HitGround();
			return;
		}

		if (CheckForCatch( ClownId.Little )) {
			return;
		}
		if (CheckForCatch(ClownId.Big)) {
			return;
		}
	}

	bool CheckForCatch (ClownId clownId) {
		var clown = PlayerRegistry.Instance.GetClown( clownId ).GetComponent<PlayerBallInteraction>();
		var distance = Vector2.Distance( clown.transform.position, transform.position );
		if (distance <= BalanceValues.Instance.CatchDistance) {
			clown.CatchBall();
			return true;
		}
		return false;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		var contact = collision.contacts[0];

		var snakeSegment = contact.collider.GetComponent<SnakeSegment>();
		if (snakeSegment != null) {
			Ball.Instance.OnSnakeCollision( snakeSegment );
		}
		else {
			Direction = Vector2.Reflect( Direction, collision.contacts[0].normal );
			Rigidbody.velocity = Direction * Ball.Instance.Speed;
		}
	}
}
