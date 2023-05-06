using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CollisionResult
{
    public bool IsColliding { get; set; }
    public bool IsWallRight { get; private set; }
    public CollisionResult(bool isColliding, bool isWallRight)
    {
        IsColliding = isColliding;
        IsWallRight = isWallRight;
    }
}
