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
        WaitForBlock,
        Lost
    }
    public GameObject board;
    private List<GameObject> blocks = new();
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
        enabled = false;
		SwitchState(States.InstantiateBlock);
	}
	private void Update()
	{
		InvokeOnGameRestart();
	}
	private void SwitchState(States state)
    {
        switch (state)
        {
            case States.InstantiateBlock:

				if (FullLineHandler.GetHighestHeight() > ConstSettingsManager.BoardHeight)
				{
					SwitchState(States.Lost);
					break;
				}

				Debug.Log("InstantiateBlock");
                InstantiateBlock();
                SwitchState(States.WaitForBlock);

                break;
            case States.WaitForBlock:

                Debug.Log("WaitForBlock");
				Debug.Log(FullLineHandler.GetHighestHeight());
				FullLineHandler.HandleDestroyingCubes();

				break;
            case States.Lost:

                Debug.Log("Lost");
				
				ResetGame();
				EventManager.OnGameOver.Invoke();
				enabled = true;

				break;
        }
    }
	private void InvokeOnGameRestart()
	{
		if (InputManager.Instance.GetTryAgain())
		{
			enabled = false;
			EventManager.OnGameRestart.Invoke();
			SwitchState(States.InstantiateBlock);
		}
	}
	public void ResetGame()
	{
		foreach (var block in blocks)
		{
			Destroy(block);
		}
		blocks = new();
	}
	private void InstantiateBlock()
    {
        GameObject block = Instantiate(DrawBlock());
        block.name += BlocksCount++;
        block.transform.position = board.transform.position + new Vector3(board.transform.position.x, 25.5f, 0);

		blocks.Add(block);
	}
    private GameObject DrawBlock()
    {
        return PrefabManager.Blocks[new System.Random().Next(PrefabManager.Blocks.Length - 1)];
    }
}
