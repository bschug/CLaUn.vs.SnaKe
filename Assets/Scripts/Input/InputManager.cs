using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class InputManager : SingletonMonoBehaviour<InputManager>
{

	//private float LittleHorizontalAxis = Input.GetAxisRaw( "Horizontal" );
	//private float LittleVerticalAxis = Input.GetAxisRaw( "Vertical" );

	//private float BigHorizontalAxis = Input.GetAxisRaw( "Horizontal2" );
	//private float BigVerticalAxis = Input.GetAxisRaw( "Vertical2" );

	public float LittleX {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Left.X;
			}
			else { return Input.GetAxisRaw( "Horizontal" ); }
		}
	}
	public float LittleY {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Left.Y;
			}
			else {
				return Input.GetAxisRaw( "Vertical" );
				;
			}
		}
	}
	public ButtonState LittleThrow { get { return GamePad.GetState( PlayerIndex.One ).Buttons.LeftShoulder; } }

	public float BigX {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Right.X;
			}
			else {
				return Input.GetAxisRaw( "Horizontal2" );
				;
			}
		}
	}

	public float BigY {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Right.Y;
			}
			else {
				return Input.GetAxisRaw( "Vertical2" );
			}
		}
	}

	public ButtonState BigThrow { get { return GamePad.GetState( PlayerIndex.One ).Buttons.RightShoulder; } }

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

	void Update () {
		if (!LittleThrowPressedThisFrame && LittleThrow == ButtonState.Pressed) {
			LittleThrowPressedThisFrame = true;
		}
		else {
			LittleThrowPressedThisFrame = false;
		}

		if (!BigThrowPressedThisFrame && BigThrow == ButtonState.Pressed) {
			BigThrowPressedThisFrame = true;
		}
		else {
			BigThrowPressedThisFrame = false;
		}

	}
}
