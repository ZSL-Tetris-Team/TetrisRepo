using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	private States state;

    public GameObject board;

	public uint Score { get; private set; } = 0;
	public void AddScore(uint amount)
	{
		Score += amount;
		Debug.Log(Score);
	}

    private GameObject fallingBlockPrefab;
    private List<GameObject> blocks = new();
	private List<GameObject> heldedBlocks = new();
	public int BlocksCount { get; private set; } = 0;

	public int FloorMask { get; private set; }
	public int WallMask { get; private set; }

	public PrefabManager PrefabManager { get; private set; }
    public ConstSettingsManager ConstSettingsManager { get; private set; }
    private void Awake()
    {
        FloorMask = LayerMask.GetMask("FloorLayer", "CubeLayer");
        WallMask = LayerMask.GetMask("WallLayer", "CubeLayer");
        PrefabManager = Resources.Load<PrefabManager>("PrefabManager");
        ConstSettingsManager = Resources.Load<ConstSettingsManager>("ConstSettingsManager");
        EventManager.OnBlockFloorCollision.AddListener(() => {
			int cubesCount = blocks.Last().GetComponentsInChildren<Transform>().Length - 1;
			AddScore((uint)cubesCount * ConstSettingsManager.SingleCubeValue);
			SwitchState(States.InstantiateBlock); 
		});
		EventManager.OnGameManagerDependeciesLoaded.Invoke();
	}
	private void Start()
	{
		SwitchState(States.InstantiateBlock);
	}
	private void Update()
	{
		switch (state)
		{
			case States.InstantiateBlock:

				break;
			case States.WaitForBlock:

				HandleHoldingBlocks();

				break;
			case States.Lost:

				InvokeOnGameRestart();

				break;
		}
	}
	private void SwitchState(States state)
    {
        switch (state)
        {
            case States.InstantiateBlock:
				this.state = States.InstantiateBlock;

				if (FullLineHandler.GetHighestHeight() > ConstSettingsManager.BoardHeight - 1)
				{
					SwitchState(States.Lost);
					break;
				}

				Debug.Log("InstantiateBlock");
				float y = FullLineHandler.GetHighestHeight() > ConstSettingsManager.BoardHeight - 6 ? 21.5f : 18.5f;
				InstantiateBlock(DrawBlock(), new Vector3(0, y, 0));
                SwitchState(States.WaitForBlock);

                break;
            case States.WaitForBlock:
				this.state = States.WaitForBlock;

                Debug.Log("WaitForBlock");
				Debug.Log(FullLineHandler.GetHighestHeight());
				FullLineHandler.HandleDestroyingCubes();

				break;
            case States.Lost:
				this.state = States.Lost;

                Debug.Log("Lost");
				
				ResetGame();
				EventManager.OnGameOver.Invoke();

				break;
        }
    }
	private void HandleHoldingBlocks()
	{
		if (InputManager.Instance.GetHoldPiece())
		{
			if(heldedBlocks.Count <= 3) 
			{
				Destroy(blocks.Last());
			
				heldedBlocks.Add(fallingBlockPrefab);
				EventManager.OnHeldedBlocksChange.Invoke(heldedBlocks);
				EventManager.OnDisableAllBlocks.Invoke();

				SwitchState(States.InstantiateBlock);
			}
			else
			{
				SwitchFallingBlock(heldedBlocks[0]);
			}
		}
	}
	private void SwitchFallingBlock(GameObject block)
	{
		Vector3 oldBlockPos = blocks.Last().transform.position;
		Destroy(blocks.Last());
		InstantiateBlock(block, oldBlockPos);
	}
	private void InvokeOnGameRestart()
	{
		if (InputManager.Instance.GetTryAgain())
		{
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
	private void InstantiateBlock(GameObject pBlock, Vector3 position)
    {
		fallingBlockPrefab = pBlock;

		GameObject block = Instantiate(pBlock);
		block.name += BlocksCount++;
		block.transform.position = board.transform.position + new Vector3(-0.5f, 0, 0) + position;

		blocks.Add(block);
	}
    private GameObject DrawBlock()
    {
		fallingBlockPrefab = PrefabManager.Blocks[new System.Random().Next(PrefabManager.Blocks.Length - 1)];
		return fallingBlockPrefab;
    }
}
