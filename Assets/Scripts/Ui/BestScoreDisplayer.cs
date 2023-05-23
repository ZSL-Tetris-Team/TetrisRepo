using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BestScoreDisplayer : MonoBehaviour
{
	private TextMeshProUGUI _textMeshPro;
	private void Awake()
	{
		_textMeshPro = GetComponent<TextMeshProUGUI>();
		_textMeshPro.text = "BEST SCORE: " + GameManager.Instance.LocalDataManager.Scores.OrderByDescending(num => num).FirstOrDefault();
	}
}
