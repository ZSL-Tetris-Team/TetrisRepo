using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
	//Initialization events
	public static Event OnGameManagerDependeciesLoaded = new();

	//Update events
	public static Event OnBlockFloorCollision = new();
	public static Event OnGameOver = new();
	public static Event OnGameRestart = new();
}
