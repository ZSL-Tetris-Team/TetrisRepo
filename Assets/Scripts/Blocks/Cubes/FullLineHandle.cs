using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FullLineHandle : MonoBehaviour
{
	private static readonly Dictionary<string, List<FullLineHandle>> lineHandleScripts = new();

	[SerializeField] private GameObject grandParent;
	private int Height
	{
		get => Mathf.RoundToInt(transform.position.y);
	}
	private void Awake()
	{
		
	}
	public static bool IsLineFull(int lineHeight, string parentName)
	{
		return lineHandleScripts[parentName].Where(lineScript => lineScript.Height == lineHeight).Count() == GameManager.Instance.BoardWidth;
	}
	private void AddToList()
	{
		if (!lineHandleScripts.ContainsKey(grandParent.name))
			lineHandleScripts[grandParent.name] = new List<FullLineHandle>();

		lineHandleScripts[grandParent.name].Add(this);
	}
}
