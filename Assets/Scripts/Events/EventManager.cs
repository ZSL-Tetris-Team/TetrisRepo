using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singelton<EventManager>
{
	//Initialization events
	public readonly Event OnGameManagerDependeciesLoaded = new();

	//Update events
	public readonly Event OnBlockFloorCollision = new();
	public readonly Event OnDisableAllBlocks = new();
	public readonly Event OnGameOver = new();
	public readonly Event OnGameRestart = new();
	public readonly Event<List<GameObject>> OnHeldedBlocksChange = new();
	public readonly Event<uint> OnScoreChange = new();
	public readonly Event OnLineStartFalling = new();
	public readonly Event OnLineFallen = new();
}
