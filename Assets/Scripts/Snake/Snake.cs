using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour
{
	public List<SnakeSegment> Segments = new List<SnakeSegment>();
	SnakeSegment Head { get { return Segments[0]; } }

	SnakePath CurrentPath;

	public IEnumerator Spawn (float delay) {
		yield return new WaitForSeconds( delay );
		CurrentPath = SnakePathRegistry.Instance.GetUnusedPath();
		SnakePathRegistry.Instance.LockPath( CurrentPath );

		CurrentPath.ShowDigAtStart();
		yield return new WaitForSeconds( BalanceValues.Instance.SnakeSpawnDigDelay );
		SoundManager.Instance.SnakeSpawned();

		var SpawnQueue = new Queue<SnakeSegment>( Segments );
		while (SpawnQueue.Count > 0) {
			var nextSegment = SpawnQueue.Dequeue();
			nextSegment.transform.position = CurrentPath.StartPosition;
			nextSegment.gameObject.SetActive( true );
			nextSegment.Path = CurrentPath;
			while (nextSegment.transform.position == CurrentPath.StartPosition) {
				yield return null;
			}
		}

		CurrentPath.HideDigAtStart();
	}

	public bool IsCompletelyUnderground { get { return !Segments.Any( s => s.gameObject.activeSelf ); } }

	public void NotifySegmentUnderground(SnakeSegment segment) {
		if (IsCompletelyUnderground) {
			ReleasePath();
		}
	}

	void ReleasePath() {
		if (!IsCompletelyUnderground) {
			Debug.LogError( "Releasing path while still above ground!" );
		}
		SnakePathRegistry.Instance.UnlockPath( CurrentPath );
		StartCoroutine( Spawn( BalanceValues.Instance.SnakeRespawnDelay ) );
	}

	public List<SnakeSegment> DetachSegmentsBehind (SnakeSegment segment) {
		var segmentIndex = Segments.IndexOf( segment );
		var result = Segments.Skip(segmentIndex + 1).ToList();
		Segments = Segments.Take( segmentIndex ).ToList();
		return result;
	}

	public void CheckForDeath() {
		if (Segments.Count == 0) {
			WinningConditionManager.Instance.SnakeDestroyed( this );
			SnakePathRegistry.Instance.UnlockPath( CurrentPath );
		}
	}
}
