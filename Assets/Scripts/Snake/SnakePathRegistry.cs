using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SnakePathRegistry : SingletonMonoBehaviour<SnakePathRegistry>
{
	public SnakePath[] Paths;
	List<SnakePath> UsedPaths = new List<SnakePath>();

	public SnakePath GetUnusedPath() {
		var availablePaths = Paths.Where( p => !UsedPaths.Contains( p ) ).ToArray();
		if (availablePaths.Length == 0) {
			Debug.LogError( "No path available!" );
			availablePaths = Paths;
		}

		var index = Random.Range( 0, availablePaths.Length );
		return availablePaths[index];
	}

	public void LockPath (SnakePath path) {
		UsedPaths.Add( path );
	}

	public void UnlockPath (SnakePath path) {
		UsedPaths.Remove( path );
	}
}
