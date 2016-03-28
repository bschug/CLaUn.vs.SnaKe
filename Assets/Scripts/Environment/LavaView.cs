using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class LavaView : MonoBehaviour {

	public SpriteRenderer GridSprite;
	public SpriteRenderer LavaSprite;

	BoxCollider2D Collider;

	void Start() {
		LavaManager.Instance.RegisterLava( this );
		Collider = GetComponent<BoxCollider2D>();
	}

	public IEnumerator Co_ShowLava(float showDuration) {
		var startTime = Time.time;
		while (Time.time - startTime < showDuration) {
			var t = (Time.time - startTime) / showDuration;
			var color = LavaSprite.color;
			color.a = Mathf.Lerp( 0, 1, t );
			LavaSprite.color = color;
			yield return null;
		}

		GridSprite.enabled = false;
		Collider.enabled = true;
	}

	public IEnumerator Co_HideLava (float hideDuration) {
		Collider.enabled = false;
		GridSprite.enabled = true;

		var startTime = Time.time;
		while (Time.time - startTime < hideDuration) {
			var t = (Time.time - startTime) / hideDuration;
			var color = LavaSprite.color;
			color.a = Mathf.Lerp( 1, 0, t );
			LavaSprite.color = color;
			yield return null;
		}
	}
}
