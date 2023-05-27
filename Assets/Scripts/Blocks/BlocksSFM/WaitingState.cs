using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitingState : BlockState
{
	private BlockFSMBase FSMBase;
	public override void Start(BlockFSMBase b)
	{
		Debug.Log(b.gameObject.name + ": WaitingState");
		FSMBase = b;

		DisableBoxColliders();
		DisableFullLineScripts();
	}
	private void DisableBoxColliders()
	{
		foreach (var col in FSMBase.GetComponentsInChildren<BoxCollider>())
		{
			col.enabled = true;
		}
	}
	private void DisableFullLineScripts()
	{
		foreach (var lineHandler in FSMBase.GetComponentsInChildren<FullLineHandler>())
		{
			lineHandler.PermDisabled = false;
		}
	}
}

