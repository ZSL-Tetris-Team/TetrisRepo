using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
	//Initialization events
	public static Event OnGameManagerDependeciesLoaded = new();

	//Update events
	public static Event OnBlockFloorCollision = new();
}
