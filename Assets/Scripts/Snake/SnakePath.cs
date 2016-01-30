using UnityEngine;
using System.Collections;

public class SnakePath : MonoBehaviour {
	public Transform[] Waypoints;
	public GameObject DigAtStart;
	public GameObject DigAtEnd;

	public Vector3 StartPosition { get { return Waypoints[0].position; } }

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
}
