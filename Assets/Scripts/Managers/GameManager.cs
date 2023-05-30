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

	private List<GameObject> nextBlocks = new();
	private GameObject fallingBlockPrefab;
	private GameObject ghostBlock;
	private List<GameObject> blocks = new();
	private GameObject heldedBlock;
	private bool hasBlockBeenHolded;
	private bool isLineFalling;
	public int BlocksCount { get; private set; }
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

		EventManager.Instance.OnLineStartFalling.AddListener(() => { isLineFalling = true; });
		EventManager.Instance.OnLineFallen.AddListener(() => { isLineFalling = false; });
		EventManager.Instance.OnGameManagerDependeciesLoaded.Invoke();
		EventManager.Instance.OnBlockFloorCollision.AddListener(() => {

			int cubesCount = blocks.Last().GetComponentsInChildren<Transform>().Length - 1;
			AddScore((uint)cubesCount * ConstSettingsManager.SingleCubeValue);
			SwitchState(States.InstantiateBlock);

		});
	}
	private void Start()
	{
		FillNextBlocks();
		SwitchState(States.InstantiateBlock);
	}
	private void SwitchState(States state)
	{
		switch (state)
		{
			case States.InstantiateBlock:
				this.state = States.InstantiateBlock;

				hasBlockBeenHolded = false;

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

				CreateBlock();

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

				Scores.AddScore(new Score(Score));
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

				CreateBlock();

				SwitchState(States.WaitForBlock);

				break;
			case States.WaitForBlock:

				HandleHoldingBlocks();

				HandleDisplayingGhost();

				break;
			case States.Lost:

				break;
		}
	}
	private Vector3 GetBlockStartPosition()
	{
		float y = FullLineHandler.GetHighestHeight() > ConstSettingsManager.BoardHeight - 6 ? 21.5f : 18.5f;
		return new Vector3(0, y, 0);
	}
	private void CreateBlock()
	{
		InstantiateBlock(nextBlocks[0], GetBlockStartPosition());

		UpdateNextBlock();
	}
	private void UpdateNextBlock()
	{
		nextBlocks.Add(DrawBlock());
		nextBlocks.RemoveAt(0);

		EventManager.Instance.OnNextBlocksChange.Invoke(nextBlocks);
	}
	private void FillNextBlocks()
	{
		for (int i = 0; i < 3; i++)
		{
			nextBlocks.Add(DrawBlock());
		}
	}
	private void HandleDisplayingGhost()
	{
		if (ghostBlock == null)
		{
			ghostBlock = Instantiate(fallingBlockPrefab);

			Debug.Log("GhostInstantiated: " + ghostBlock.name);

			BlockFSMBase b = ghostBlock.GetComponent<BlockFSMBase>();

			b.StartBlock(b.GhostState);
		}

		ghostBlock.transform.position = Collision.GetClosestBottomPoint(blocks.Last());
		ghostBlock.transform.rotation = blocks.Last().transform.rotation;
	}
	private void HandleHoldingBlocks()
	{
		if (InputManager.Instance.GetHoldPiece())
		{
			if (hasBlockBeenHolded) return;

			hasBlockBeenHolded = true;

			if (heldedBlock == null)
			{
				Destroy(ghostBlock);
				ghostBlock = null;

				Destroy(blocks.Last());
				blocks.RemoveAt(blocks.Count - 1);

				heldedBlock = fallingBlockPrefab;

				EventManager.Instance.OnHeldedBlocksChange.Invoke(heldedBlock);

				CreateBlock();
			}
			else
			{
				SwitchFallingBlock();
			}
		}
	}
	private void SwitchFallingBlock()
	{
		Destroy(ghostBlock);
		ghostBlock = null;
		
		Destroy(blocks.Last());
		blocks.RemoveAt(blocks.Count - 1);

		GameObject oldHeldedBlock = heldedBlock;
		heldedBlock = fallingBlockPrefab;
		EventManager.Instance.OnHeldedBlocksChange.Invoke(heldedBlock);

		InstantiateBlock(oldHeldedBlock, GetBlockStartPosition());
	}
	private GameObject InstantiateBlock(GameObject pBlock, Vector3 position)
	{
		fallingBlockPrefab = pBlock;

		GameObject block = Instantiate(pBlock);
		block.name += BlocksCount++;
		block.transform.position = board.transform.position + new Vector3(-0.5f, 0, 0) + position;

		blocks.Add(block);

		Debug.Log("Instantiated: " + block.name);

		BlockFSMBase b = block.GetComponent<BlockFSMBase>();
		b.StartBlock(b.FallingState);

		return block;
	}
	private GameObject DrawBlock()
	{
		return PrefabManager.Blocks[new System.Random().Next(PrefabManager.Blocks.Length)];
	}
}