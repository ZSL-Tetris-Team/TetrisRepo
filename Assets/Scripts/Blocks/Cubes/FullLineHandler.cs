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
	[SerializeField] private Vector3 position;
	[SerializeField] private float realHeight;
	[SerializeField] private int _height;
	private int Height
	{
		get
		{
			Vector3 pos = transform.TransformPoint(col.center);
			position = pos;
			realHeight = pos.y;
			int height = (int)Math.Round(pos.y - 0.5f);
			//transform.position = new Vector3(transform.position.x, height, transform.position.z);
			_height = height;
			return height;
		}
	}
	private void Awake()
	{
		EventManager.OnGameManagerDependeciesLoaded.AddListener(AssignDependencies);
		col = GetComponent<BoxCollider>();
		//EventManager.OnBlockFloorCollision.AddListener(() => { Debug.Log(Height); });
	}
	private void Start()
	{
		lineHandlerScripts.Add(this);
	}
	private void OnDestroy()
	{
		lineHandlerScripts.Remove(this);
	}
	private void AssignDependencies()
	{
		gameManager = GameManager.Instance;
		csm = gameManager.ConstSettingsManager;
	}
	private static bool IsLineFull(int height)
	{
		int count = lineHandlerScripts.Where(lineScript => lineScript.Height == height).Count();
		Debug.Log($"Count: {count} | Height: {height}");
		return count == GameManager.Instance.ConstSettingsManager.BoardWidth;
	}
	public static void DebugHeight()
	{
		var heights = lineHandlerScripts.Select(lineHander => lineHander.Height).Distinct().ToList();

		foreach(float height in heights)
		{
			Debug.Log(height);
		}
	}
	public static void HandleDestroyingCubes()
	{
		List<int> heights = lineHandlerScripts.Select(lineHander => lineHander.Height).Distinct().OrderBy(number => number).ToList();

		foreach (int height in heights)
		{
			if (IsLineFull(height))
			{
				foreach (var lineHander in lineHandlerScripts.Where(lineScript => lineScript.Height == height).ToList())
				{
					Destroy(lineHander.gameObject);
				}

				foreach (var lineHandler in lineHandlerScripts)
				{
					if (lineHandler.Height > height)
					{
						lineHandler.transform.position += new Vector3(0, -1, 0);
					}
				}
			}
		}
	}
}
