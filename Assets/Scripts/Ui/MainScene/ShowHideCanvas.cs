using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideCanvas : MonoBehaviour
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
