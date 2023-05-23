using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstSettingsManager", menuName = "ScriptableObjects/ConstSettingsManager")]
public class ConstSettingsManager : ScriptableObject
{
    [Header("Board")]
	[SerializeField] private int _boardWidth = 10;
	[SerializeField] private int _boardHeight = 20;

    [Header("BlockMovement")]
	[SerializeField][Range(0.1f, 1)] private float _fallTime = 1;
    [SerializeField][Range(0.1f, 1)] private float _softDropFallTime = 0.5f;
    [SerializeField] private float _horizontalBlockSpeed = 6;
	[SerializeField] private float _hardDropBlockSpeed = 6;
    [SerializeField] private float _verticalBlockFallSpeed = 10;

    [Header("Score")]
    [SerializeField] private uint _fullLineValue = 100;
    [SerializeField] private uint _singleCubeValue = 10;

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
	public uint FullLineValue { get => _fullLineValue; private set => _fullLineValue = value; }
	public uint SingleCubeValue { get => _singleCubeValue; private set => _singleCubeValue = value; }
}
