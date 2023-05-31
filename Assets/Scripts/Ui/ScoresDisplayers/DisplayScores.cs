using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DisplayScores : MonoBehaviour
{
	private void Awake()
	{
		CreateHeader();

		for (int i = 0; i < Scores.List.Count; i++)
		{
			Score score = Scores.List[i];

			GameObject go = new(i.ToString());
			go.transform.SetParent(transform, false);

			TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
			text.autoSizeTextContainer = true;
			text.text = $"{i + 1}	{score.ToString(true)}";

			text.color = i switch
			{
				0 => Color.green,
				1 => Color.yellow,
				2 => Color.red,
				_ => Color.cyan,
			};
		}
	}
	private void CreateHeader()
	{
		GameObject header = new("Header");
		header.transform.SetParent(transform, false);

		TextMeshProUGUI text = header.AddComponent<TextMeshProUGUI>();
		text.autoSizeTextContainer = true;
		text.text = "TOP	DATE			      SCORE";
		text.fontWeight = FontWeight.Heavy;
		text.color = Color.cyan;
	}
}
