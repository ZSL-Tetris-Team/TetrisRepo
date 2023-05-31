using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Score
{
    private DateTime _date;
    private uint _points;

    public DateTime Date { get => _date; set => _date = value; }
    public uint Points { get => _points; set => _points = value; }
    public Score(uint points)
    {
        _date = DateTime.Now;
        _points = points;
	}
	public Score(DateTime date, uint points)
	{
		_date = date;
		_points = points;
	}
	public string ToString(bool prettier = false)
    {
        if (prettier) return $"{Date}   {Points}";
		return $"{Date},{Points}";
    }
    public static Score Parse(string s)
    {
        string[] values = s.Split(',');
        return new Score(DateTime.Parse(values[0]), uint.Parse(values[1]));
    }
}
