using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Scores
{
    public const string fileDirectory = "Assets/Data/Files/Scores.txt";
    private static readonly List<uint> scores = new();
    private static void LoadScores()
    {
        File.Create(fileDirectory).Close();
		using (StreamReader stream = File.OpenText(fileDirectory))
        {
            string line;
            while((line = stream.ReadLine()) != null)
            {
                scores.Add(uint.Parse(line));
            }
        }
    }
    /// <summary>
    /// Ads the score to the list and saves it in the txt file
    /// </summary>
    public static void AddScore(uint score)
    {
        if(scores.Count == 0) LoadScores();

        scores.Add(score);
        File.AppendAllLines(fileDirectory, new string[] { Convert.ToString(score) });
    }
    public static uint GetHighest()
    {
		if (scores.Count == 0) LoadScores();
		return scores.OrderByDescending(x => x).FirstOrDefault();
    }
	public static int Length()
    {
		if (scores.Count == 0) LoadScores();
		return scores.Count();
    }
    public static uint GetLast()
    {
		if (scores.Count == 0) LoadScores();
        return scores.Last();
	}
}
