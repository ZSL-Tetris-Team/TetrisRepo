using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabManager", menuName = "ScriptableObjects/PrefabManager", order = 1)]
public class PrefabManager : ScriptableObject
{
    [SerializeField]private GameObject[] _blocks;
    public GameObject[] Blocks
    {
        get => _blocks;
        private set => _blocks = value;
    }
}
