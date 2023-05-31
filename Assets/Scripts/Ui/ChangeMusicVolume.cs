using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusicVolume : MonoBehaviour
{
	private Slider slider;
	private void Start()
	{ 
		slider = GetComponent<Slider>();
		slider.value = PlayerPrefs.GetFloat("MusicVolume");
		slider.onValueChanged.AddListener((volume) => { AudioManager.Instance.MusicVolume = volume; });
		slider.onValueChanged.Invoke(slider.value);
	}
}
