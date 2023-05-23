using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalDataManager", menuName = "ScriptableObjects/LocalDataManager")]
public class LocalDataManager : ScriptableObject
{
    [SerializeField] private List<uint> _scores = new List<uint>();

	public List<uint> Scores { get => _scores; private set => _scores = value; }
	public void AddScore(uint score)
	{
		Scores.Add(score);
	}
}
