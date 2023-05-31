using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
