using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Scores
{
    public const string fileDirectory = "Assets/Data/Files/Scores.txt";
    private static List<Score> _scores = new();

	public static List<Score> List { 
        get
        {
			if (_scores.Count == 0) LoadScores();
			return _scores.OrderByDescending(num => num.Points).ToList();
        }
    }

	private static void LoadScores()
    {
		using (StreamReader stream = File.OpenText(fileDirectory))
        {
            string line;
            //string line = stream.ReadLine();
            //if (line == null) return;
            while((line = stream.ReadLine()) != null)
            {
                //Debug.Log(line);
                _scores.Add(Score.Parse(line));
            }
        }
    }
    /// <summary>
    /// Ads the score to the list and saves it in the txt file
    /// </summary>
    public static void AddScore(Score score)
    {
        if(_scores.Count == 0) LoadScores();

        //Debug.Log(score.ToString());

        _scores.Add(score);
        File.AppendAllLines(fileDirectory, new string[] { score.ToString() });
    }
    public static Score GetHighest()
    {
		if (_scores.Count == 0) LoadScores();
		return _scores.OrderByDescending(x => x.Points).FirstOrDefault();
    }
	public static int Length()
    {
		if (_scores.Count == 0) LoadScores();
		return _scores.Count();
    }
    public static Score GetLast()
    {
		if (_scores.Count == 0) LoadScores();
        return _scores.Last();
	}
}
