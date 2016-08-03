using UnityEngine;
using System.Collections;

public class SuddenDeath : MonoBehaviour {

	public GameObject StonePrefab;

	GameObject SurvivingClown;

	float MinDelayBetweenStones { get { return BalanceValues.Instance.SuddenDeathStonesMinDelay; } }
	float MaxDelayBetweenStones { get { return BalanceValues.Instance.SuddenDeathStonesMaxDelay; } }

	void Start () {
		SurvivingClown = PlayerRegistry.Instance.GetClown( ClownId.Little );
		if (!SurvivingClown.GetComponent<PlayerHealth>().IsAlive) {
			SurvivingClown = PlayerRegistry.Instance.GetClown( ClownId.Big );
		}

		StartCoroutine( Co_ThrowStones() );
	}
	
	IEnumerator Co_ThrowStones() {
		while (true) {
			yield return new WaitForSeconds( Random.Range( MinDelayBetweenStones, MaxDelayBetweenStones ) );
			var stone = GameObject.Instantiate( StonePrefab ).GetComponent<SuddenDeathStone>();
			stone.TargetPosition = SurvivingClown.transform.position;
		}
	}
}
