using Mono.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClipping : MonoBehaviour
{
    private static readonly Dictionary<string,List<WallClipping>> wallClippings = new();
	private BoxCollider col;
	[SerializeField] private GameObject parent;

	private void Awake()
	{
		col = GetComponent<BoxCollider>();
	}
	private void Start()
	{
		if (!wallClippings.ContainsKey(parent.name))
			wallClippings[parent.name] = new List<WallClipping>();

		wallClippings[parent.name].Add(this);
	}
	private void OnDestroy()
	{
		wallClippings[parent.name].Remove(this);
	}
	public static bool IsWallClipping(string parentName)
	{
		foreach(WallClipping clipping in wallClippings[parentName])
		{
			if (clipping.IsClipping()) return true;
		}
		return false;
	}
	private bool IsClipping()
	{
		Ray[] rays =
		{
			new Ray(transform.TransformPoint(col.center) - new Vector3(0.5f, 0, 0), Vector3.right),
			new Ray(transform.TransformPoint(col.center) + new Vector3(0.5f, 0, 0), Vector3.left),
		};

		foreach(Ray ray in rays)
		{
			Color color = Physics.Raycast(ray, 1, LayerMask.GetMask("WallLayer"))? Color.red : Color.green;
			Debug.DrawRay(ray.origin, ray.direction * 1, color);
		}

		foreach(Ray ray in rays)
		{
			if (Physics.Raycast(ray, 1, LayerMask.GetMask("WallLayer"))) return true;
		}

		return false;
	}
}
