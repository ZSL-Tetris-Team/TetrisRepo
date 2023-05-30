using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSFXVolume : MonoBehaviour
{
	private Slider slider;
	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = PlayerPrefs.GetFloat("SFXVolume");
		slider.onValueChanged.AddListener((volume) => { AudioManager.Instance.SFXVolume = volume; });
		slider.onValueChanged.Invoke(slider.value);
	}
}
