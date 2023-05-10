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
	private float horizontalSpeed;
	private float verticalDistanceToTravel = 0;
	[SerializeField] private Vector3 position;
	[SerializeField] private float realHeight;
	[SerializeField] private int _height;
	[SerializeField] private bool isInvisible;
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
		col = GetComponent<BoxCollider>();
		gameManager = GameManager.Instance;
		csm = gameManager.ConstSettingsManager;
		horizontalSpeed = csm.HorizontalBlockSpeed;
		//EventManager.OnBlockFloorCollision.AddListener(EnableLineHandler);
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
		lineHandlerScripts.Remove(this);
	}
	private void HandleDistanceToTravel()
	{
		if (verticalDistanceToTravel > 0)
		{
			transform.position += new Vector3(0, -Time.deltaTime * horizontalSpeed, 0);
			verticalDistanceToTravel -= Time.deltaTime * horizontalSpeed;
		}
	}
	private static bool IsLineFull(int height)
	{
		int count = lineHandlerScripts.Where(lineScript => lineScript.Height == height && !lineScript.isInvisible).Count();
		Debug.Log($"Count: {count} | Height: {height}");
		return count == GameManager.Instance.ConstSettingsManager.BoardWidth;
	}
	public static void DebugHeight()
	{
		var heights = lineHandlerScripts.Where(lineHander => !lineHander.isInvisible).Select(lineHander => lineHander.Height).Distinct().ToList();

		foreach(float height in heights)
		{
			Debug.Log(height);
		}
	}
	public static void HandleDestroyingCubes()
	{
		List<int> heights = lineHandlerScripts.Select(lineHander => lineHander.Height).Distinct().OrderByDescending(number => number).ToList();

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
						lineHandler.verticalDistanceToTravel++;
					}
				}
			}
		}
	}
}
