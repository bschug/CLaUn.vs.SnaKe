using UnityEngine;
using System.Collections;

public class BalanceValues : SingletonMonoBehaviour<BalanceValues> {
	public float PlayerSpeed;

	public float JuggleDistance;
	public float CatchDistance;
	public float PickupDistance;
	public float[] BallSpeed;
	public float BallBounceDuration;

	public float SnakeSpeed;
	public int SnakeSize;
	public float InitialSnakeSpawnDelay;
	public float SnakeRespawnDelay;
	public float SnakeSpawnDigDelay;
}
