using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstSettingsManager", menuName = "ScriptableObjects/ConstSettingsManager")]
public class ConstSettingsManager : ScriptableObject
{
    [SerializeField] private GameObject board;
    [SerializeField][Range(0.1f, 1)] private float _fallTime = 1;
    [SerializeField][Range(0.1f, 1)] private float _softDropFallTime = 0.5f;
	[SerializeField]private int _boardWidth = 10;
	[SerializeField] private int _boardHeight = 20;
    [SerializeField] private float _horizontalBlockSpeed = 6;
	[SerializeField] private float _hardDropBlockSpeed = 6;
    [SerializeField] private float _verticalBlockFallSpeed = 10;

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
        private set => _horizontalBlockSpeed = value; 
    }
	public float HardDropBlockSpeed { 
        get => _hardDropBlockSpeed; 
        private set => _hardDropBlockSpeed = value; 
    }
	public float VerticalBlockFallSpeed { 
        get => _verticalBlockFallSpeed; 
        set => _verticalBlockFallSpeed = value; 
    }
	public GameObject Board { 
        get => board;
        set => board = value; 
    }
}
