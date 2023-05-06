using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : Singelton<GameManager>
{
    private enum States
    {
        InstantiateBlock,
        WaitForBlock
    }
    private States state;
    public PrefabManager PrefabManager { get; private set; }
    public ConstSettingsManager ConstSettingsManager { get; private set; }
    public int FloorMask { get; private set; }
    public int WallMask { get; private set; }
    private void Awake()
    {
        FloorMask = LayerMask.GetMask("FloorLayer", "CubeLayer");
        WallMask = LayerMask.GetMask("WallLayer", "CubeLayer");
        PrefabManager = Resources.Load<PrefabManager>("PrefabManager");
        ConstSettingsManager = Resources.Load<ConstSettingsManager>("ConstSettingsManager");
    }
    private void Start()
    {
        SwitchState(States.InstantiateBlock);
        EventManager.OnBlockFloorCollision.AddListener(() => { SwitchState(States.InstantiateBlock); });
    }
    private void SwitchState(States state)
    {
        switch (state)
        {
            case States.InstantiateBlock:
                Debug.Log("InstantiateBlock");
                PrefabUtility.InstantiatePrefab(DrawBlock());
                SwitchState(States.WaitForBlock);
                break;
            case States.WaitForBlock:
                Debug.Log("WaitForBlock");
                //kod jeszcze nie zaimplementowany
                break;
        }
    }
    private GameObject DrawBlock()
    {
        return PrefabManager.Blocks[new System.Random().Next(PrefabManager.Blocks.Length - 1)];
    }
}
