using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRegistry : SingletonMonoBehaviour<PlayerRegistry>
{
	Dictionary<ClownId, GameObject> Players = new Dictionary<ClownId, GameObject>();

	public GameObject GetClown (ClownId clownId) {
		return Players[clownId];
	}

	public GameObject GetOtherClown (GameObject clown) {
		var clownId = clown.GetComponent<Player>().ClownId;
		return GetClown( clownId );
	}

	public void RegisterPlayer (Player player) {
		Players.Add( player.ClownId, player.gameObject );
	}

	public void RemovePlayer (Player player) {
		Players.Remove( player.ClownId );
	}
	
}
