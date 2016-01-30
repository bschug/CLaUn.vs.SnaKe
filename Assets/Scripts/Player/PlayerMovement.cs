using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public ClownId ClownId;

	float Speed { get { return BalanceValues.Instance.PlayerSpeed; } }

	void Update() {
		var movement = InputManager.Instance.GetMovement( ClownId );
		UpdatePosition( movement );
	}

	void UpdatePosition (Vector2 movement) {
		transform.Translate( movement * Speed * Time.deltaTime );
	}
}
