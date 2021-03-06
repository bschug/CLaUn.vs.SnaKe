﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
	public float LittleX {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Left.X;
			}
			else { return Input.GetAxisRaw( "Red Horizontal" ); }
		}
	}
	public float LittleY {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Left.Y;
			}
			else {
				return Input.GetAxisRaw( "Red Vertical" );
				;
			}
		}
	}
	public ButtonState LittleThrow {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).Buttons.LeftShoulder;
			}
			else {
				return Input.GetButton( "Red Throw" ) ? ButtonState.Pressed : ButtonState.Released;
			}
		}
	}

	public float BigX {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).ThumbSticks.Right.X;
			}
			else {
				return Input.GetAxisRaw( "Blue Horizontal" );
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
				return Input.GetAxisRaw( "Blue Vertical" );
			}
		}
	}

	public ButtonState BigThrow {
		get {
			if (GamePad.GetState( PlayerIndex.One ).IsConnected) {
				return GamePad.GetState( PlayerIndex.One ).Buttons.RightShoulder;
			}
			else {
				return Input.GetButton( "Blue Throw" ) ? ButtonState.Pressed : ButtonState.Released;
			}
		}
	}

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

	void Update () {
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
