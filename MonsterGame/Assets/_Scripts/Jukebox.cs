using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Jukebox : MonoBehaviour
{
	[SerializeField] private List<AudioClip> _musicClips = new();

	private AudioSource _audioSource;

	public static Jukebox instance { get; private set; }

	private bool _shouldBePlaying = false;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			_audioSource = GetComponent<AudioSource>();
			_audioSource.loop = true;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(gameObject);
	}

	public void PlayRandom()
	{
		if (_audioSource.isPlaying)
		{
			Stop(PlayRandomInternal);
		}
		else PlayRandomInternal();
		
		void PlayRandomInternal()
		{
			float volume = PlayerPrefs.GetFloat("Music", 1f);
			_audioSource.clip = _musicClips[Random.Range(0, _musicClips.Count)];
			_audioSource.volume = 0;
			_audioSource.DOFade(volume, 1);
			_audioSource.Play();
			_shouldBePlaying = true;
		}
	}

	public void Update()
	{
		if (_shouldBePlaying && !_audioSource.isPlaying) PlayRandom();
	}

	public void Pause()
	{
		_audioSource.Pause();
	}

	public void Stop()
	{
		_shouldBePlaying = false;
		_audioSource.DOFade(0, 1).OnComplete(
			() => _audioSource.Stop());
	}
	
	public void Stop(Action onComplete)
	{
		_shouldBePlaying = false;
		_audioSource.DOFade(0, 1).OnComplete(
			() =>
			{
				_audioSource.Stop();
				onComplete?.Invoke();
			});
	}
	
}
