using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInitializer : MonoBehaviour
{
    private void Awake()
    {
        _ = GameManager.Instance;
    }
}
