using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class BlockFSMBase : MonoBehaviour
{
	private BoxCollider _col;
	private GameManager _gameManager;
	private InputManager _inputManager;
	private ConstSettingsManager _csm;

	private BlockState _fallingState = new FallingState();
	private BlockState _waitingState = new WaitingState();
	private BlockState _disabledState = new DisabledState();
	private BlockState _ghostState = new GhostState();

	private BlockState currState;
	[SerializeField] private string currStateName;

	public BoxCollider Col { get => _col; private set => _col = value; }
	public GameManager GameManager { get => _gameManager; private set => _gameManager = value; }
	public InputManager InputManager { get => _inputManager; private set => _inputManager = value; }
	public ConstSettingsManager Csm { get => _csm; private set => _csm = value; }
	public BlockState FallingState { get => _fallingState; private set => _fallingState = value; }
	public BlockState WaitingState { get => _waitingState; private set => _waitingState = value; }
	public BlockState DisabledState { get => _disabledState; private set => _disabledState = value; }
	public BlockState GhostState { get => _ghostState; private set => _ghostState = value; }
	public BlockState CurrState
	{
		get => currState;
		private set
		{
			currState = value;
			currStateName = currState.ToString();
		}
	}
	private void Awake()
	{
		Col = GetComponent<BoxCollider>();
		GameManager = GameManager.Instance;
		InputManager = InputManager.Instance;
		Csm = GameManager.ConstSettingsManager;

		enabled = false;
	}
	public void StartBlock(BlockState startState)
	{
		CurrState = startState;
		CurrState.Start(this);
		enabled = true;
	}
	private void Update()
	{
		if (Time.timeScale == 0) return;
		CurrState.Update(this);
	}
	private void FixedUpdate()
	{
		CurrState.FixedUpdate(this);
	}
	private void OnDestroy()
	{
		CurrState.OnDestroy(this);
	}
	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
		CurrState.OnCollisionEnter(collision);
	}
	public void SwitchState(BlockState state)
	{
		enabled = false;
		BlockState oldState = CurrState;

		CurrState = state;
		oldState.Exit(this);
		CurrState.Start(this);
	}
}
