using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreOverlay : MonoBehaviour
{
	[SerializeField] private string message = "Your Score";
	private TextMeshProUGUI _textMeshPro;
	private void Awake()
	{
		_textMeshPro = GetComponent<TextMeshProUGUI>();
		EventManager.Instance.OnScoreChange.AddListener(UpdateScore);
	}
	private void UpdateScore(uint score)
	{
		_textMeshPro.text =$"{message}: {score}";
	}
}
