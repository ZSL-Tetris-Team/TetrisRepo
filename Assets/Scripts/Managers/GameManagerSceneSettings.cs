using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSceneSettings : MonoBehaviour
{
    [SerializeField] private GameObject board;
	public GameObject Board { get => board; set => board = value; }
}
