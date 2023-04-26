using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksMovement : MonoBehaviour
{
	private Transform transform;
	private InputManager inputManager;
	private Timer timer;
	private void Start()
	{
		transform = GetComponent<Transform>();
		inputManager = InputManager.Instance;
		VerticalMovement();
	}
	private void Update()
	{
		HorizontalMovement();
	}
	private void HorizontalMovement()
	{
		if (inputManager.GetRight())
		{
			transform.Translate(Vector3.right);
		}
		if (inputManager.GetLeft())
		{
			transform.Translate(Vector3.left);
		}
	}
	private void VerticalMovement()
	{
		transform.Translate(Vector3.down);
		timer = Timer.NewInstance;
		timer.TimeLeft = 1;
		timer.StartTimer(VerticalMovement);
	}

}
