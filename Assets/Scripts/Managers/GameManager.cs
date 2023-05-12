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
    public GameObject board;
    public PrefabManager PrefabManager { get; private set; }
    public ConstSettingsManager ConstSettingsManager { get; private set; }
    public int FloorMask { get; private set; }
    public int WallMask { get; private set; }
    public int BlocksCount { get; private set; } = 0;
    private void Awake()
    {
        FloorMask = LayerMask.GetMask("FloorLayer", "CubeLayer");
        WallMask = LayerMask.GetMask("WallLayer", "CubeLayer");
        PrefabManager = Resources.Load<PrefabManager>("PrefabManager");
        ConstSettingsManager = Resources.Load<ConstSettingsManager>("ConstSettingsManager");
        EventManager.OnBlockFloorCollision.AddListener(() => { SwitchState(States.InstantiateBlock); });
		EventManager.OnGameManagerDependeciesLoaded.Invoke();
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
                FullLineHandler.HandleDestroyingCubes();
				//FullLineHandler.DebugHeight();
				break;
        }
    }
    private void InstantiateBlock()
    {
        GameObject block = Instantiate(DrawBlock());
        block.name += BlocksCount++;
        block.transform.position = board.transform.position + new Vector3(0, 18.5f, 0);

	}
    private GameObject DrawBlock()
    {
        return PrefabManager.Blocks[new System.Random().Next(PrefabManager.Blocks.Length - 1)];
    }
}
