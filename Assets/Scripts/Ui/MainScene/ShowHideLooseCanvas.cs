using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideLooseCanvas : MonoBehaviour
{
	private void Awake()
	{
		ToggleCanvas();
		EventManager.Instance.OnGameOver.AddListener(ToggleCanvas);
	}
	private void ToggleCanvas()
	{
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}
}
