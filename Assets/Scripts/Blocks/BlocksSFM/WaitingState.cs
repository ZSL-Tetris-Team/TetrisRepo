using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitingState : BlockState
{
	public override void Start(BlockFSMBase b)
	{
		Debug.Log(b.gameObject.name + ": WaitingState");

		foreach(var col in b.GetComponentsInChildren<BoxCollider>())
		{
			col.enabled = true;
		}
		foreach(var lineHandler in b.GetComponentsInChildren<FullLineHandler>())
		{
			lineHandler.PermDisabled = false;
		}
	}
}

