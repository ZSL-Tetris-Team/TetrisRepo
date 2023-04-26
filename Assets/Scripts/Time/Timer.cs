using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer NewInstance
    {
        get
        {
            GameObject obj = new();
            obj.name = typeof(Timer).Name;
            obj.hideFlags = HideFlags.HideAndDontSave;
            return obj.AddComponent<Timer>();
        }
    }

    private bool isActive = false;
	private float _timeLeft;
	private int _timeLeftInt;
	private Action functionToCall;
	public float TimeLeft 
    {
        get { return _timeLeft; }
        set { 
            _timeLeft = value; 
            _timeLeftInt = Mathf.FloorToInt(_timeLeft);
        }
    }
    public int TimeLeftInt { get; }
	private void Update()
	{
        if (!isActive) return;
		TimeLeft -= Time.deltaTime;
        if(TimeLeft < 0)
        {
            StopTimer();
            functionToCall();
        }
	}
	public void StartTimer(Action functionToCall)
	{
        this.functionToCall = functionToCall;
		isActive = true;
	}
    public void StopTimer()
    {
        isActive = false;
    }
    public void ToggleTimer()
    {
        isActive = !isActive;
    }
}
