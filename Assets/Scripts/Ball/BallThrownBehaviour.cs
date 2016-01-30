using UnityEngine;
using System.Collections;

public class BallThrownBehaviour : MonoBehaviour
{
	public PlayerBallInteraction TargetClown;
	Rigidbody2D Rigidbody;
	Ball Ball;

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
		Ball = GetComponent<Ball>();
	}

	void Update () {
		if (TimeToReachClown <= BalanceValues.Instance.CatchDistance) {
			TargetClown.CatchBall();
		}

		var direction = TargetClown.transform.position - transform.position;
		direction.Normalize();
		Rigidbody.velocity = direction * Ball.Speed;
		
	}

	float TimeToReachClown {
		get {
			var distanceToClown = Vector2.Distance( TargetClown.transform.position, transform.position );
			return distanceToClown / Ball.Speed;
		}
	}
}
