using UnityEngine;
using System.Collections;

public class SuddenDeath : MonoBehaviour {

	public GameObject StonePrefab;

	GameObject ActivePlayer;

	float MinDelayBetweenStones { get { return BalanceValues.Instance.SuddenDeathStonesMinDelay; } }
	float MaxDelayBetweenStones { get { return BalanceValues.Instance.SuddenDeathStonesMaxDelay; } }

	void Start () {
		ActivePlayer = PlayerRegistry.Instance.GetClown( ClownId.Little );
		if (!ActivePlayer.GetComponent<PlayerHealth>().IsAlive) {
			ActivePlayer = PlayerRegistry.Instance.GetClown( ClownId.Big );
		}

		StartCoroutine( Co_ThrowStones() );
	}
	
	IEnumerator Co_ThrowStones() {
		while (true) {
			yield return new WaitForSeconds( Random.Range( MinDelayBetweenStones, MaxDelayBetweenStones ) );
			var stone = GameObject.Instantiate( StonePrefab ).GetComponent<SuddenDeathStone>();
			stone.TargetPosition = ActivePlayer.transform.position;
		}
	}
}
