using UnityEngine;
using System.Collections;
using System.Linq;

public class SnakePath : MonoBehaviour {
	public Transform[] Waypoints;
	public GameObject DigAtStart;
	public GameObject DigAtEnd;

	public Vector3 StartPosition { get { return Waypoints[0].position; } }

	public Color GizmoColor = new Color( 0, 0, 0, 0 );

	int DigAtStartCount = 0;
	int DigAtEndCount = 0;

	public void ShowDigAtStart() {
		DigAtStartCount++;
		DigAtStart.SetActive( true );
	}

	public void HideDigAtStart() {
		DigAtStartCount--;
		if (DigAtStartCount == 0) {
			DigAtStart.SetActive( false );
		}
	}

	public void ShowDigAtEnd () {
		DigAtEndCount++;
		DigAtEnd.SetActive( true );
	}

	public void HideDigAtEnd() {
		DigAtEndCount--;
		if (DigAtEndCount == 0) {
			DigAtEnd.SetActive( false );
		}
	}

	void OnDrawGizmos() {
		if (GizmoColor.a == 0) {
			GizmoColor = Color.HSVToRGB( Random.Range( 0f, 1f ), 1, 1 );
		}

		Gizmos.color = GizmoColor;
		DrawNodeGizmo( Waypoints[0] );
		for (var i=1; i < Waypoints.Length; i++) {
			DrawNodeGizmo( Waypoints[i] );
			DrawLineGizmo( Waypoints[i-1], Waypoints[i] );
		}
		DrawNodeGizmo( Waypoints.Last() );
	}

	void DrawNodeGizmo(Transform waypoint) {
		Gizmos.DrawSphere( waypoint.position, 0.1f );
	}

	void DrawLineGizmo(Transform a, Transform b) {
		Gizmos.DrawLine( a.position, b.position );
	}
}
