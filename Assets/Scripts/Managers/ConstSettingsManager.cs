using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstSettingsManager", menuName = "ScriptableObjects/ConstSettingsManager")]
public class ConstSettingsManager : ScriptableObject
{
    [SerializeField][Range(0.1f, 1)] private float _fallTime = 1;
    [SerializeField][Range(0.1f, 1)] private float _softDropFallTime = 0.5f;

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
}
