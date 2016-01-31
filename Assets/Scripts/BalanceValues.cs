using UnityEngine;
using System.Collections;

public class BalanceValues : SingletonMonoBehaviour<BalanceValues> {
	public float PlayerSpeed;
	public float StunDuration = 1;
	public int PlayerHealth = 5;

	public float JuggleDistance;
	public float CatchDistance;
	public float PickupDistance;
	public float[] BallSpeed;
	public float BallBounceDuration;
	public float LoseChargeInHandDelay;

	public float SnakeSpeed;
	public int SnakeSize;
	public float InitialSnakeSpawnDelay;
	public float SnakeRespawnDelay;
	public float SnakeSpawnDigDelay;
	public int HealthPerSegment = 3;

	public float ScreenShakeIntensitySmall;
	public float ScreenShakeIntensityBig;
    public float ScreenShakeTimeSmall;
	public float ScreenShakeTimeBig;

    public float WinScreenDuration = 5;
}
