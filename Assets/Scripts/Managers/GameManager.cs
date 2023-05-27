using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
		EventManager.Instance.OnScoreChange.Invoke(Score);
	}

    private GameObject fallingBlockPrefab;
	private GameObject ghostBlock;
    public List<GameObject> blocks = new();
	private List<GameObject> heldedBlocks = new();
	private bool isLineFalling;
	public int BlocksCount { get; private set; } = 0;
	public int FloorMask { get; private set; }
	public int WallMask { get; private set; }

	public PrefabManager PrefabManager { get; private set; }
    public ConstSettingsManager ConstSettingsManager { get; private set; }
    public LocalDataManager LocalDataManager { get; private set; }
    private void Awake()
    {
        FloorMask = LayerMask.GetMask("FloorLayer", "CubeLayer");
        WallMask = LayerMask.GetMask("WallLayer", "CubeLayer");

        PrefabManager = Resources.Load<PrefabManager>("PrefabManager");
        ConstSettingsManager = Resources.Load<ConstSettingsManager>("ConstSettingsManager");
		LocalDataManager = Resources.Load<LocalDataManager>("LocalDataManager");

       
		EventManager.Instance.OnLineStartFalling.AddListener(() => { isLineFalling = true; });
		EventManager.Instance.OnLineFallen.AddListener(() => { isLineFalling = false; });
		EventManager.Instance.OnGameManagerDependeciesLoaded.Invoke();
		blocks.Clear();
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
				this.state = States.InstantiateBlock;

				Destroy(ghostBlock);
				ghostBlock = null;

				FullLineHandler.HandleDestroyingCubes();

				if (isLineFalling) return;

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
				Debug.Log("Highest blocks height: " + FullLineHandler.GetHighestHeight());

				break;
            case States.Lost:
				this.state = States.Lost;

                Debug.Log("Lost");

				LocalDataManager.Scores.Add(Score);
				ResetGame();
				EventManager.Instance.OnGameOver.Invoke();

				break;
        }
    }
	private void Update()
	{
		switch (state)
		{
			case States.InstantiateBlock:

				if (isLineFalling) return;

				Destroy(ghostBlock);
				ghostBlock = null;

				if (FullLineHandler.GetHighestHeight() > ConstSettingsManager.BoardHeight - 1)
				{
					SwitchState(States.Lost);
					break;
				}

				float y = FullLineHandler.GetHighestHeight() > ConstSettingsManager.BoardHeight - 6 ? 21.5f : 18.5f;
				InstantiateBlock(DrawBlock(), new Vector3(0, y, 0));
				SwitchState(States.WaitForBlock);

				break;
			case States.WaitForBlock:

				HandleHoldingBlocks();

				HandleDisplayingGhost();

				break;
			case States.Lost:

				InvokeOnGameRestart();

				break;
		}
	}
	private void HandleDisplayingGhost()
	{
		if (ghostBlock == null)
		{
			ghostBlock = Instantiate(fallingBlockPrefab);

			Debug.Log("GhostInstantiated: " + ghostBlock.name);

			Debug.Log(blocks.Count);
			BlockFSMBase b = ghostBlock.GetComponent<BlockFSMBase>();
			EventManager.Instance.OnBlockFloorCollision.AddListener(() => {

				int cubesCount = blocks.Last().GetComponentsInChildren<Transform>().Length - 1;
				AddScore((uint)cubesCount * ConstSettingsManager.SingleCubeValue);
				SwitchState(States.InstantiateBlock);

			});
			//Debug.Log("disable");
			b.StartBlock(b.DisabledState);

		}
		
		ghostBlock.transform.position = Collision.GetClosestBottomPoint(blocks.Last());
		ghostBlock.transform.rotation = blocks.Last().transform.rotation;
	}
	private void HandleHoldingBlocks()
	{
		if (InputManager.Instance.GetHoldPiece())
		{
			if(heldedBlocks.Count <= 3) 
			{
				Destroy(blocks.Last());
			
				heldedBlocks.Add(fallingBlockPrefab);
				EventManager.Instance.OnHeldedBlocksChange.Invoke(heldedBlocks);
				EventManager.Instance.OnDisableAllBlocks.Invoke();

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
			EventManager.Instance.OnGameRestart.Invoke();
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

		Debug.Log(blocks.Count);
		Debug.Log("Instantiated: " + block.name);

		BlockFSMBase b = block.GetComponent<BlockFSMBase>();
		b.StartBlock(b.FallingState);
	}
    private GameObject DrawBlock()
    {
		fallingBlockPrefab = PrefabManager.Blocks[new System.Random().Next(PrefabManager.Blocks.Length)];
		return fallingBlockPrefab;
    }
}
