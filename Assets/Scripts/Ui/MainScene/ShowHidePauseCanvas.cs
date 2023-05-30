using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidePauseCanvas : MonoBehaviour
{
	private void Awake()
	{
		ToggleCanvas();
		EventManager.Instance.OnPauseGame.AddListener(ToggleCanvas);
	}
	private void ToggleCanvas()
	{
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}
}
