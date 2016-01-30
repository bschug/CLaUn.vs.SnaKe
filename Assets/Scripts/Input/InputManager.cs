using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class InputManager : SingletonMonoBehaviour<InputManager> {
	public float LittleX { get { return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Left.X; } }
	public float LittleY { get { return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Left.Y; } }
	public ButtonState LittleThrow { get { return GamePad.GetState( PlayerIndex.One ).Buttons.LeftShoulder; } }
	public float BigX { get { return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Right.X; } }
	public float BigY { get { return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Right.Y; } }
	public ButtonState BigThrow { get { return GamePad.GetState( PlayerIndex.One ).Buttons.RightShoulder; } }

	private ButtonState LittleThrowLastFrame;
	private ButtonState BigThrowLastFrame;
	private bool LittleThrowPressedThisFrame;
	private bool BigThrowPressedThisFrame;

	public Vector2 GetMovement (ClownId clownId) {
		switch (clownId) {
			case ClownId.Little:
				return new Vector2( LittleX, LittleY );
			case ClownId.Big:
				return new Vector2( BigX, BigY );
			default:
				throw new System.InvalidOperationException();
		}
	}

	public bool WasThrowPressedThisFrame (ClownId clownId) {
		switch (clownId) {
			case ClownId.Little:
				return LittleThrowPressedThisFrame;
			case ClownId.Big:
				return BigThrowPressedThisFrame;
			default:
				throw new System.InvalidOperationException();
		}
	}

	void Update() {
		if (LittleThrowLastFrame == ButtonState.Released && LittleThrow == ButtonState.Pressed) {
			LittleThrowPressedThisFrame = true;
		}
		else {
			LittleThrowPressedThisFrame = false;
		}
		LittleThrowLastFrame = LittleThrow;

		if (BigThrowLastFrame == ButtonState.Released && BigThrow == ButtonState.Pressed) {
			BigThrowPressedThisFrame = true;
		}
		else {
			BigThrowPressedThisFrame = false;
		}
		BigThrowLastFrame = BigThrow;
	}
}
