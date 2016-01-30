using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : SingletonMonoBehaviour<Ball> {

	public GameObject[] Views;
	public BallState CurrentState { get; private set; }
	public int ChargeLevel {
		get {
			if (NumJuggles >= 3) {
				return 2;
			}
			else if (NumJuggles >= 1) {
				return 1;
			}
			return 0;
		}
	}
	public int NumJuggles { get; private set; }

	[SerializeField]
	BallHeldBehaviour BallHeldBehaviour;
	[SerializeField]
	BallThrownBehaviour BallThrownBehaviour;
	[SerializeField]
	BallBouncingBehaviour BallBouncingBehaviour;
	[SerializeField]
	BallOnGroundBehaviour BallOnGroundBehaviour;

	public float Speed { get { return BalanceValues.Instance.BallSpeed[ChargeLevel]; } }

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
		GetClown( ClownId.Little ).CatchBall();
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

	public void Catch (ClownId catchingClown) {
		BallHeldBehaviour.CurrentClown = GetClown( catchingClown );
		SetState( BallState.Held );
	}

	public void Throw (ClownId throwingClown) {
		if (CurrentState != BallState.Held) {
			Debug.LogError( "Cannot throw ball when not held" );
			return;
		}
		if (BallHeldBehaviour.CurrentClown.ClownId != throwingClown) {
			Debug.LogError( "This clown doesn't hold the ball!" );
			return;
		}

		NumJuggles = 0;
		BallThrownBehaviour.TargetClown = GetClown( throwingClown.Other() );
		SetState( BallState.Thrown );
	} 

	public void Juggle (ClownId throwingClown) {
		if (CurrentState == BallState.Held) {
			Debug.LogError( "Cannot juggle ball when held" );
			return;
		}
		if (CurrentState == BallState.OnGround) {
			Debug.LogError( "Cannot juggle ball when on ground" );
			return;
		}
		Debug.Log( "Juggling to " + throwingClown.Other() );

		NumJuggles++;
		BallThrownBehaviour.TargetClown = GetClown( throwingClown.Other() );
		SetState( BallState.Thrown );
	}

	PlayerBallInteraction GetClown (ClownId clownId) {
		return PlayerRegistry.Instance.GetClown(clownId).GetComponent<PlayerBallInteraction>();
	}

}
