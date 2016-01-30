using UnityEngine;
using System.Collections;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T :SingletonMonoBehaviour<T> {

    public static T Instance { get; private set; }

    void Awake()
    {
		if (Instance != null) {
			Debug.LogError( "Duplicate instance of " + name );
			Destroy( this );
		}
		Instance = (T)this;
    }

	void OnDestroy() {
		if (Instance == this) {
			Instance = null;
		}
	}
}
