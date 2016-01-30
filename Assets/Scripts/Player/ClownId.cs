using UnityEngine;
using System.Collections;

public enum ClownId {
	None,
	Little,
	Big
}

public static class ClownIdExtensions
{
	public static ClownId Other (this ClownId clownId) {
		switch (clownId) {
			case ClownId.Little: return ClownId.Big;
			case ClownId.Big: return ClownId.Little;
			default: throw new System.ArgumentException();
		}
	}
}
