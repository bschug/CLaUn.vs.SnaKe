using UnityEngine;
using System.Collections;

public class SnakeSegment : MonoBehaviour
{
	public SnakeSegment Leader;
	public SnakePath Path;
	public float Radius = 0.4f;
	public float EPSILON = 0.01f;
	public float DigDelay = 0.75f;

	public Snake Snake { get { return transform.parent.GetComponent<Snake>(); } }
	public bool IsHead { get { return Leader == null; } }
	public bool IsDigging { get; private set; }
	public Vector2 Direction { get; private set; }

	int NextWaypointId = 0;
	Rigidbody2D Rigidbody;

	Vector3 NextWaypoint { get { return Path.Waypoints[NextWaypointId].transform.position; } }
	float Speed { get { return BalanceValues.Instance.SnakeSpeed; } }
	float DistanceToWaypoint { get { return Vector2.Distance( NextWaypoint, transform.position ); } }
	float DistanceToLeader { get { return Vector2.Distance( Leader.transform.position, transform.position ); } }
	bool OverlapsLeader { get { return DistanceToLeader <= Radius + Leader.Radius; } }

	void Awake() {
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	void OnEnable() {
		IsDigging = false;
		NextWaypointId = 0;
	}

	void FixedUpdate() {
		if (IsDigging) {
			return; // Handled by Coroutine below
		}
		if (IsHead) {
			MoveToNextWaypoint();
		}
		else {
			FollowLeader();
		}
	}

	void MoveToNextWaypoint() {
		Direction = (NextWaypoint - transform.position).normalized;
		var targetPos = (Vector2)transform.position + Direction * Speed * Time.deltaTime;
		Rigidbody.MovePosition( targetPos );
		if (DistanceToWaypoint <= Radius) {
			SelectNextWaypoint();
		}
	}

	void FollowLeader() {
		Direction = (Leader.transform.position - transform.position).normalized;
		if (Leader.IsDigging) {
			var movement = Direction * Mathf.Min(DistanceToLeader, Speed * Time.deltaTime);
			var targetPos = (Vector2)transform.position + movement;
			Rigidbody.MovePosition( targetPos );
			if (DistanceToLeader <= EPSILON) {
				Snake.StartCoroutine( Dig() );
			}
		}
		else if (!OverlapsLeader) {
			var targetPos = (Vector2)(Leader.transform.position) - Direction * (Leader.Radius + Radius);
			Rigidbody.MovePosition( targetPos );
		}
	}

	void SelectNextWaypoint() {
		if (NextWaypointId + 1 < Path.Waypoints.Length) {
			NextWaypointId++;
		}
		else {
			StartCoroutine( Dig() );
		}
	}

	IEnumerator Dig() {
		IsDigging = true;
		Path.ShowDigAtEnd();
		yield return new WaitForSeconds( DigDelay );
		Path.HideDigAtEnd();
		gameObject.SetActive( false );
		Snake.NotifySegmentUnderground( this );
	}

}
