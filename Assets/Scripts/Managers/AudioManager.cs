using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singelton<AudioManager>
{
    public AudioMixersData AudioMixersData { get; private set; }
	private float _musicVolume, _SFXVolume;
	public float MusicVolume
	{
		get => _musicVolume;
		set
		{
			_musicVolume = Mathf.Log10(value) * 20;
			AudioMixersData.MusicMixer.SetFloat("MusicVolume", _musicVolume);
			PlayerPrefs.SetFloat("MusicVolume", value);
		}
	}
	public float SFXVolume
	{
		get => _SFXVolume;
		set
		{
			_SFXVolume = Mathf.Log10(value) * 20;
			AudioMixersData.SFXMixer.SetFloat("SFXVolume", _SFXVolume);
			PlayerPrefs.SetFloat("SFXVolume", value);
		}
	}
	private void Awake()
	{
		AudioMixersData = Resources.Load<AudioMixersData>("AudioMixersData");
		MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
		SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
	}
	
}