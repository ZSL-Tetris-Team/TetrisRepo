using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Event OnBlockFloorCollision = new();
    public static Event<GameManager.States> SwitchGameManagerState= new();
}
