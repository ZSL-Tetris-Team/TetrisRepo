using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMixersData", menuName = "ScriptableObjects/AudioMixersData")]
public class AudioMixersData : ScriptableObject
{
	[SerializeField] private AudioMixer _musicMixer;
	[SerializeField] private AudioMixer _SFXMixer;

	public AudioMixer MusicMixer { get => _musicMixer; private set => _musicMixer = value; }
	public AudioMixer SFXMixer { get => _SFXMixer; private set => _SFXMixer = value; }
}
