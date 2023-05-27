using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class FullLineHandler : MonoBehaviour
{
	private static readonly List<FullLineHandler> lineHandlerScripts = new();

	private GameManager gameManager;
	private ConstSettingsManager csm;
	private BoxCollider col;
	private float verticalSpeed;
	private float verticalDistanceToTravel = 0;
	[SerializeField] private Vector3 realPosition;
	[SerializeField] private float realHeight;
	[SerializeField] private int _height;
	[SerializeField] private bool isInvisible;
	private bool _permDisabled;
    public bool PermDisabled 
	{ 
		get => _permDisabled; 
		set 
		{
			_permDisabled = value;
			enabled = !value;
		}
	}
    private int Height
	{
		get
		{
			Vector3 pos = transform.TransformPoint(col.center);
			realPosition = pos;
			realHeight = pos.y;

			int height = (int)Math.Round(pos.y);
			_height = height;

			return height;
		}
	}
	private void Awake()
	{
		col = GetComponent<BoxCollider>();
		gameManager = GameManager.Instance;
		csm = gameManager.ConstSettingsManager;
		verticalSpeed = csm.VerticalBlockSpeed;
		EventManager.Instance.OnBlockFloorCollision.AddListener(EnableLineHandler);
	}
	private void Start()
	{
		lineHandlerScripts.Add(this);
	}
	private void Update()
	{
		HandleDistanceToTravel();
	}
	private void OnDestroy()
	{
		EventManager.Instance.OnBlockFloorCollision.RemoveListener(EnableLineHandler);
		if (PermDisabled) return;
		lineHandlerScripts.Remove(this);
	}
	private void EnableLineHandler()
	{
		if (PermDisabled) return;
		enabled = true;
	}
	private void HandleDistanceToTravel()
	{
		if (verticalDistanceToTravel > 0)
		{
			transform.position += new Vector3(0, -verticalSpeed, 0);
			verticalDistanceToTravel -= verticalSpeed;
		}
		if (verticalDistanceToTravel < 0)
		{
			verticalDistanceToTravel = 0;
			transform.position = new Vector3(transform.position.x, (float)Math.Round(transform.position.y), transform.position.z);
			EventManager.Instance.OnLineFallen.Invoke();
		}
	}
	private static bool IsLineFull(int height)
	{
		int count = lineHandlerScripts.Where(lineScript => lineScript.Height == height && lineScript.enabled).Count();
		//Debug.Log($"Count: {count} | Height: {height}");
		return count == GameManager.Instance.ConstSettingsManager.BoardWidth;
	}
	public static void DebugHeight()
	{
		var heights = lineHandlerScripts.Where(lineHander => lineHander.enabled).Select(lineHander => lineHander.Height).Distinct().ToList();
		foreach(float height in heights)
		{
			Debug.Log(height);
		}
	}
	public static int GetHighestHeight()
	{
		return lineHandlerScripts.Where(lineHandler => lineHandler.enabled).Select(lineHandler => lineHandler.Height).OrderByDescending(number => number).FirstOrDefault();
	}
	public static void HandleDestroyingCubes()
	{
		List<int> heights = lineHandlerScripts.Select(lineHander => lineHander.Height).Distinct().OrderByDescending(number => number).ToList();

		foreach (int height in heights)
		{
			if (IsLineFull(height))
			{
				GameManager.Instance.AddScore(GameManager.Instance.ConstSettingsManager.FullLineValue);
				EventManager.Instance.OnLineStartFalling.Invoke();

				foreach (var lineHander in lineHandlerScripts.Where(lineScript => lineScript.Height == height).ToList())
				{
					Destroy(lineHander.gameObject);
				}

				foreach (var lineHandler in lineHandlerScripts)
				{
					if (lineHandler.Height > height)
					{
						lineHandler.verticalDistanceToTravel++;
					}
				}
			}
		}
	}
}
