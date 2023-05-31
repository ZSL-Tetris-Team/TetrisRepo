using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
	private TextMeshProUGUI _textMeshPro;
	private void Start()
	{
		Debug.Log("display");
		_textMeshPro = GetComponent<TextMeshProUGUI>();
		_textMeshPro.text = "SCORE: " + GameManager.Instance.Score;
	}
}
