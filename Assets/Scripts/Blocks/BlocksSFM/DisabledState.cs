using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledState : BlockState
{
	private BlockFSMBase FSMBase;

	private GameManager gameManager;
	private ConstSettingsManager csm;
	public override void Start(BlockFSMBase b)
	{
		Debug.Log(b.gameObject.name + ": DisabledState");

		FSMBase = b;

		gameManager = b.GameManager;
		csm = gameManager.ConstSettingsManager;

		DisableFullLineScripts();
		DisableBoxColliders();
	}
	private void DisableFullLineScripts()
	{
		foreach (var lineHandler in FSMBase.GetComponentsInChildren<FullLineHandler>())
		{
			lineHandler.PermDisabled = true;
		}
	}
	private void DisableBoxColliders()
	{
		foreach (var col in FSMBase.GetComponentsInChildren<BoxCollider>())
		{
			col.enabled = false;
		}
	}

}
