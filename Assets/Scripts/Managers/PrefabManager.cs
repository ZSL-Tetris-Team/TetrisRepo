using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabManager", menuName = "ScriptableObjects/PrefabManager", order = 1)]
public class PrefabManager : ScriptableObject
{
    [SerializeField]private GameObject[] _blocks;
    [SerializeField] private GameObject _burstCubeParticle;
    [SerializeField] private GameObject _blowCubeParticle;
    public GameObject[] Blocks
    {
        get => _blocks;
        private set => _blocks = value;
    }
	public GameObject BurstCubeParticle { get => _burstCubeParticle; private set => _burstCubeParticle = value; }
	public GameObject BlowCubeParticle { get => _blowCubeParticle; private set => _blowCubeParticle = value; }
}
