using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstSettingsManager", menuName = "ScriptableObjects/ConstSettingsManager")]
public class ConstSettingsManager : ScriptableObject
{
    [SerializeField][Range(0.1f, 1)] private float _fallTime = 1;
    [SerializeField][Range(0.1f, 1)] private float _softDropFallTime = 0.5f;
	[SerializeField]private int _boardWidth = 10;
	[SerializeField] private int _boardHeight = 20;
    [SerializeField] private float _horizontalBlockSpeed = 6;

	public float FallTime
    {
        get => _fallTime;
        private set => _fallTime = value;
    }
    public float SoftDropFallTime
    {
        get => _softDropFallTime;
        private set => _softDropFallTime = value;
    }
    public int BoardWidth
    {
        get => _boardWidth;
        private set => _boardWidth = value;
    }
    public int BoardHeight
    {
        get => _boardHeight;
        private set => _boardHeight = value;
    }
	public float HorizontalBlockSpeed { 
        get => _horizontalBlockSpeed; 
        set => _horizontalBlockSpeed = value; }
}
