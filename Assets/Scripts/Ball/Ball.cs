using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : SingletonMonoBehaviour<Ball> {

	public GameObject[] Views;
	public BallState CurrentState { get; private set; }

	[SerializeField]
	BallHeldBehaviour BallHeldBehaviour;
	[SerializeField]
	BallThrownBehaviour BallThrownBehaviour;
	[SerializeField]
	BallBouncingBehaviour BallBouncingBehaviour;
	[SerializeField]
	BallOnGroundBehaviour BallOnGroundBehaviour;

	MonoBehaviour BehaviourForState (BallState state) {
		switch (state) {
			case BallState.Held: return BallHeldBehaviour;
			case BallState.Thrown: return BallThrownBehaviour;
			case BallState.Bouncing: return BallBouncingBehaviour;
			case BallState.OnGround: return BallOnGroundBehaviour;
			default: throw new System.InvalidOperationException();
		}
	}

	override protected void Awake() {
		base.Awake();
		BallHeldBehaviour.CurrentClown = PlayerRegistry.Instance.GetClown( ClownId.Little );
		GetClown( ClownId.Little ).CatchBall();
		SetState( BallState.Held );
	}

	void SetState (BallState state) {
		if (CurrentState == state) {
			return;
		}
		if (CurrentState != BallState.None) {
			BehaviourForState( CurrentState ).enabled = false;
		}
		BehaviourForState( state ).enabled = true;
		CurrentState = state;
	}

	public void Throw (ClownId throwingClown) {
		Debug.Log( "Ball thrown by " + throwingClown.ToString() );
		//var thrown = BehaviourForState( BallState.Thrown );
		//if (CurrentState == BallState.Held) {
			
		//}
	} 

	public void Juggle (ClownId throwingClown) {
		Debug.Log( "Ball juggled by " + throwingClown.ToString() );
	}

	PlayerBallInteraction GetClown (ClownId clownId) {
		return PlayerRegistry.Instance.GetComponent<PlayerBallInteraction>();
	}
}
