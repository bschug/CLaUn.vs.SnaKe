using UnityEngine;

namespace Vector2Extensions
{
	public static class Extensions
	{
		public static Vector2 OrthogonalCCW (this Vector2 v) {
			return new Vector2( -v.y, v.x );
		}

		public static Vector2 OrthogonalCW (this Vector2 v) {
			return new Vector2( v.y, -v.x );
		}
	}

}