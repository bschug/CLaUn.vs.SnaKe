using System.Collections.Generic;
using UnityEngine;

public class SnakeFactory : SingletonMonoBehaviour<SnakeFactory>
{
	public SnakeSegment SegmentPrefab;

	int SnakeSize { get { return BalanceValues.Instance.SnakeSize; } }
	float InitialSnakeSpawnDelay { get { return BalanceValues.Instance.InitialSnakeSpawnDelay; } }

	public void SpawnSnake() {
		Snake snake = CreateEmptySnake();
		SnakeSegment previousSegment = null;
		for (var i = 0; i < SnakeSize; i++) {
			var segment = GameObject.Instantiate( SegmentPrefab ).GetComponent<SnakeSegment>();
			segment.Leader = previousSegment;
			segment.gameObject.SetActive( false );
			segment.gameObject.layer = snake.gameObject.layer;
			segment.transform.parent = snake.transform;
			segment.RefreshView();
			snake.Segments.Add( segment );
			previousSegment = segment;
		}
		snake.StartCoroutine( snake.Spawn( InitialSnakeSpawnDelay ) );
	}

	public Snake CreateEmptySnake() {
		var go = new GameObject();
		go.name = "Snake";
		go.AddComponent<Snake>();
		go.transform.parent = this.transform;
		go.layer = this.gameObject.layer;
		WinningConditionManager.Instance.SnakeSpawned( go.GetComponent<Snake>() );
		return go.GetComponent<Snake>();
	}

	public Snake CreateFromSegments(List<SnakeSegment> segments) {
		var snake = CreateEmptySnake();
		snake.Segments = segments;
		for(var i=0; i < segments.Count; i++) {
			segments[i].transform.parent = snake.transform;
			segments[i].RefreshView();
		}
		return snake;
	}
}