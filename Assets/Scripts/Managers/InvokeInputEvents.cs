using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeInputEvents : MonoBehaviour
{
    void Update()
    {
		if (InputManager.Instance.GetPause())
		{
			EventManager.Instance.OnPauseGame.Invoke();
			Time.timeScale = Time.timeScale == 1? 0 : 1;
		}
	}
}
