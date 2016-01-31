using UnityEngine;
using System.Collections;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

	AudioSource AudioSource;
	public AudioClip[] SwooshSounds;
	public AudioClip[] BooSounds;
	public AudioClip[] CheerSounds;
	public AudioClip[] BigCheerSounds;

	public AudioClip[] PlayerHitSounds;
	public AudioClip[] PlayerHitSlapSounds;

	public AudioClip[] SnakeSpawnSounds;
	public AudioClip[] SnakeHitNoDmgSounds;
	public AudioClip[] SnakeDamagedSounds;
	public AudioClip[] SnakeSegmentDestroySounds;

	protected override void Awake () {
		base.Awake();
		AudioSource = GetComponent<AudioSource>();
	}

	void PlaySoundFromArray(AudioClip[] sounds, float volumeScale = 1f) {
		if (sounds.Length == 0) {
			Debug.LogWarning( "No sounds defined!" );
			return;
		}
		AudioSource.PlayOneShot( sounds[Random.Range( 0, sounds.Length )] );
	}

	public void Swoosh() { PlaySoundFromArray( SwooshSounds ); }
	public void Boo() { PlaySoundFromArray( BooSounds ); }
	public void Cheer() { PlaySoundFromArray( CheerSounds ); }
	public void BigCheer() { PlaySoundFromArray( BigCheerSounds ); }
	public void PlayerHit() {
		PlaySoundFromArray( PlayerHitSounds, 2 );
		PlaySoundFromArray( PlayerHitSlapSounds, 2 );
	}
	public void SnakeSpawned() { PlaySoundFromArray( SnakeSpawnSounds ); }
	public void SnakeHitNoDmg() { PlaySoundFromArray( SnakeHitNoDmgSounds ); }
	public void SnakeDamaged() { PlaySoundFromArray( SnakeDamagedSounds ); }
	public void SnakeSegmentDestroyed() { PlaySoundFromArray( SnakeSegmentDestroySounds ); }
}
