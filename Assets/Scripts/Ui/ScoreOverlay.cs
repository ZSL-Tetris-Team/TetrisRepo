using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreOverlay : MonoBehaviour
{
	private TextMeshProUGUI _textMeshPro;
	private void Awake()
	{
		_textMeshPro = GetComponent<TextMeshProUGUI>();
		EventManager.OnScoreChange.AddListener(UpdateScore);
	}
	private void UpdateScore(uint score)
	{
		_textMeshPro.text = "Twój wynik: " + score;
	}
}
