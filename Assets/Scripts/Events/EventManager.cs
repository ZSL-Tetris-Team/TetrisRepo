using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
	//Initialization events
	public static readonly Event OnGameManagerDependeciesLoaded = new();

	//Update events
	public static readonly Event OnBlockFloorCollision = new();
	public static readonly Event OnDisableAllBlocks = new();
	public static readonly Event OnGameOver = new();
	public static readonly Event OnGameRestart = new();
	public static readonly Event<List<GameObject>> OnHeldedBlocksChange = new();
	public static readonly Event<uint> OnScoreChange = new();
}
