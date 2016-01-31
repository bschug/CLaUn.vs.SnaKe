using UnityEngine;
using System.Collections;

public class SuddenDeathStone : MonoBehaviour {

	public Vector2 TargetPosition;
	Rigidbody2D Rigidbody;
	float Speed;
	float EnableColliderDelay = 0.5f;

	float Top = 5;
	float Left = -9.5f;
	float Bottom = -5;
	float Right = 9.5f;
	float Width { get { return Right - Left; } }
	float Height { get { return Top - Bottom; } }

	float MinSpeed { get { return BalanceValues.Instance.SuddenDeathStoneMinSpeed; } }
	float MaxSpeed { get { return BalanceValues.Instance.SuddenDeathStoneMaxSpeed; } }

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	void Start () {
		transform.position = GetRandomStartPosition();
		Speed = Random.Range( MinSpeed, MaxSpeed );
		Invoke( "EnableCollider", EnableColliderDelay );

		var direction = (TargetPosition - (Vector2)transform.position).normalized;
		Rigidbody.velocity = direction * Speed;

	}

	void OnCollisionEnter2D (Collision2D collision) {
		Destroy( this );
	}

	Vector2 GetRandomStartPosition() {
		if (Random.Range(0,3) == 0) {
			// vertical walls
			return new Vector2( Left + Random.Range( 0, 2 ) * Width, Bottom + Random.Range( 0f, 1f ) * Height );
		}
		else {
			// horizontal walls
			return new Vector2( Left + Random.Range( 0f, 1f ) * Width, Bottom + Random.Range( 0, 2 ) * Height );
		}
	}

	void EnableCollider() {
		GetComponent<CircleCollider2D>().enabled = true;
	}
}
