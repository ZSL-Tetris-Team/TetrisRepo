using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singelton<InputManager>
{
	private Input _input;
	private void Awake()
	{
		_input = new();
	}
	private void OnEnable()
	{
		_input.Enable();
	}
	private void OnDisable()
	{
		_input.Disable();
	}
	public bool GetRight()
	{
		return _input.Blocks.Right.triggered;
	}
	public bool GetLeft() 
	{
		return _input.Blocks.Left.triggered;
	}
}

