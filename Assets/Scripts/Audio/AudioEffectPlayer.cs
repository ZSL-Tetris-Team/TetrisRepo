using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectPlayer : MonoBehaviour
{
    public static AudioEffectPlayer Instance;
	private AudioSource audioSource;
	public static AudioClip loadingClip;
	private void Awake()
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
	}
	public void PlayAudioEffect(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
}
