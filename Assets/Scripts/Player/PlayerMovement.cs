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
		GetComponent<Rigidbody2D>().velocity = movement * Speed * Time.deltaTime;
	}
}
