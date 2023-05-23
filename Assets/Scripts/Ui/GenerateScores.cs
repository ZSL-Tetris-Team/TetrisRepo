using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GenerateScores : MonoBehaviour
{
	[SerializeField] private GameObject textPrefab;
	private List<GameObject> textes = new();
	//private void FixedUpdate()
	//{
	//	foreach (var t in textes)
	//	{
	//		Destroy(t);
	//	}

	//	var scores = Resources.Load<LocalDataManager>("LocalDataManager").Scores;
	//	for (int i = 0; i < scores.Count; i++)
	//	{
	//		GameObject t = Instantiate(textPrefab);
	//		textes.Add(t);
	//		t.GetComponent<TextMeshProUGUI>().text = i + ". " + scores[i];
	//	}
	//}
}
