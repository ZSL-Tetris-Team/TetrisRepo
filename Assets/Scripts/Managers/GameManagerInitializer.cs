using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInitializer : MonoBehaviour
{
    [SerializeField] private GameObject board;
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.board = board;
    }
}
