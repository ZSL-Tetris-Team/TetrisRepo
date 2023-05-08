using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : Singelton<GameManager>
{
    public enum States
    {
        InstantiateBlock,
        WaitForBlock
    }
    public PrefabManager PrefabManager { get; private set; }
    public ConstSettingsManager ConstSettingsManager { get; private set; }
    public int FloorMask { get; private set; }
    public int WallMask { get; private set; }
    public int BlocksCount { get; private set; } = 0;
    public int BoardWidth { get; private set; } = 8;
    private void Awake()
    {
        FloorMask = LayerMask.GetMask("FloorLayer", "CubeLayer");
        WallMask = LayerMask.GetMask("WallLayer", "CubeLayer");
        PrefabManager = Resources.Load<PrefabManager>("PrefabManager");
        ConstSettingsManager = Resources.Load<ConstSettingsManager>("ConstSettingsManager");
        EventManager.OnBlockFloorCollision.AddListener(() => { SwitchState(States.InstantiateBlock); });
	}
	private void Start()
	{
		SwitchState(States.InstantiateBlock);
	}
	private void SwitchState(States state)
    {
        switch (state)
        {
            case States.InstantiateBlock:
                Debug.Log("InstantiateBlock");
                InstantiateBlock();
                SwitchState(States.WaitForBlock);
                break;
            case States.WaitForBlock:
                Debug.Log("WaitForBlock");
                //kod jeszcze nie zaimplementowany
                break;
        }
    }
    private void InstantiateBlock()
    {
		PrefabUtility.InstantiatePrefab(DrawBlock()).name += BlocksCount++;
	}
    private GameObject DrawBlock()
    {
        return PrefabManager.Blocks[new System.Random().Next(PrefabManager.Blocks.Length - 1)];
    }
}
