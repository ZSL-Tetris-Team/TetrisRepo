using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private float shakeTimerTotal;
	private float shakeTimer;
	private float startingIntensity;
	public static CameraShake Instance { private set; get; }
	private CinemachineVirtualCamera virtualCamera;
	private CinemachineBasicMultiChannelPerlin multiChanelPerlin;

	private void Awake()
	{
		Instance = this;
		virtualCamera = GetComponent<CinemachineVirtualCamera>();
		multiChanelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}
	private void Update()
	{
		if(shakeTimer > 0)
		{
			shakeTimer -= Time.deltaTime;
			multiChanelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0, 1 - (shakeTimer / shakeTimerTotal));
		}
	}
	public void ShakeCamera(float intensity, float frequency, float time)
	{
		startingIntensity = intensity;
		multiChanelPerlin.m_FrequencyGain = frequency;
		shakeTimerTotal = time;
		shakeTimer = time;
	}
}
