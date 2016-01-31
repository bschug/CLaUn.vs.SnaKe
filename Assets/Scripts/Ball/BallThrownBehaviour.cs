using UnityEngine;
using System.Collections;

public class BallThrownBehaviour : MonoBehaviour
{
	public float MaxTorque = 3;

	public PlayerBallInteraction TargetClown;
	Rigidbody2D Rigidbody;
	Ball Ball;

	Vector2 Direction { get { return (TargetClown.transform.position - transform.position).normalized; } }

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
		Ball = GetComponent<Ball>();
	}

	void OnEnable() {
		Rigidbody.isKinematic = false;
		Rigidbody.AddTorque( Random.Range( -MaxTorque, MaxTorque ) );
		SoundManager.Instance.Swoosh();
	}

	void Update () {
		if (DistanceToClown <= BalanceValues.Instance.CatchDistance || 
			TimeToReachClown <= Time.smoothDeltaTime) {
			TargetClown.CatchBall();
		}
		
		Rigidbody.velocity = Direction * Ball.Speed;
		
	}

	float DistanceToClown { get { return Vector2.Distance( TargetClown.transform.position, transform.position ); } }
	float TimeToReachClown { get { return DistanceToClown / Ball.Speed; } }

	void OnCollisionEnter2D (Collision2D collision) {
		var contact = collision.contacts[0];

		var snakeSegment = contact.collider.GetComponent<SnakeSegment>();
		if (snakeSegment != null) {
			Ball.Instance.OnSnakeCollision( snakeSegment );
		}
		else {
			var direction = Vector2.Reflect( Direction, collision.contacts[0].normal ).normalized;
			Rigidbody.velocity = direction * Ball.Instance.Speed;
			Ball.Instance.OnObstacleHit( direction );
		}
	}
}
