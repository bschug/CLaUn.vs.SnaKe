using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRegistry : SingletonMonoBehaviour<PlayerRegistry> {

	public GameObject LittleClown;
	public GameObject BigClown;

	Dictionary<ClownId, GameObject> Players = new Dictionary<ClownId, GameObject>();

	public GameObject GetClown (ClownId clownId) {
		return Players[clownId];
	}

	public GameObject GetOtherClown (GameObject clown) {
		var clownId = clown.GetComponent<PlayerMovement>().ClownId;
		return GetClown( clownId );
	}

	protected override void Awake () {
		base.Awake();
		Players[ClownId.Little] = LittleClown;
		Players[ClownId.Big] = BigClown;
	}
}
