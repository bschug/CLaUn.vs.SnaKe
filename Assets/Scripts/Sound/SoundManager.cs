using UnityEngine;
using System.Collections;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

	AudioSource AudioSource;
	public AudioClip[] SwooshSounds;
	public AudioClip[] BooSounds;
	public AudioClip[] CheerSounds;

	protected override void Awake () {
		base.Awake();
		AudioSource = GetComponent<AudioSource>();
	}

	void PlaySoundFromArray(AudioClip[] sounds) {
		AudioSource.PlayOneShot( sounds[Random.Range( 0, sounds.Length )] );
	}

	public void Swoosh() {
		PlaySoundFromArray( SwooshSounds );
	}

	public void Boo() {
		PlaySoundFromArray( BooSounds );
	}

	public void Cheer() {
		PlaySoundFromArray( CheerSounds );
	}
}
