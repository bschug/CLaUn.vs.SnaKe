using UnityEngine;
using System.Collections;

public class HealthUI : MonoBehaviour {

	public ClownId ClownId;
	public GameObject[] HealthIcons;

	PlayerHealth PlayerHealth;

	void Awake() {
		PlayerHealth = PlayerRegistry.Instance.GetClown( ClownId ).GetComponent<PlayerHealth>();
	}

	void Update () {
		for (int i=0; i < HealthIcons.Length; i++) {
			HealthIcons[i].SetActive( PlayerHealth.Health > i );
		}
	}
}
