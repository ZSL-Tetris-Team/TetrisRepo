using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLooseCanvas : MonoBehaviour
{
	private void Awake()
	{
		EventManager.OnGameOver.AddListener(ToggleVisibility);
		EventManager.OnGameRestart.AddListener(ToggleVisibility);
		gameObject.SetActive(false);
	}
	private void ToggleVisibility()
	{
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}
}
