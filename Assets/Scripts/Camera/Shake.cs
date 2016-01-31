using UnityEngine;
using System.Collections;

public class Shake : SingletonMonoBehaviour<Shake>
{


	public float screenShakeTimeLeft = .1f;
	public float screenShakeIntensity;
	public float screenShakeTime;


	void Update () {
		screenShakeTimeLeft -= Time.deltaTime;
		ScreenShake();
	}

	public void ScreenShake () {
		if (screenShakeTimeLeft > 0) {
			var percentLeft = screenShakeTimeLeft / screenShakeTime;
			Camera.main.transform.position = (Vector3)Random.insideUnitCircle * screenShakeIntensity * percentLeft + Vector3.forward * Camera.main.transform.position.z;
			screenShakeTimeLeft -= Time.deltaTime;

			//if (screenShakeTimeLeft <= 0) {
			//	Camera.main.transform.position = Vector3.forward * Camera.main.transform.position.z;
			//}
		}
		else {
			Camera.main.transform.position = Vector3.forward * Camera.main.transform.position.z;
		}

		
	}

	public void StartShake(float duration, float intensity ) {

		screenShakeTimeLeft = duration;
		screenShakeTime = duration;
		screenShakeIntensity = intensity;

	}


}
