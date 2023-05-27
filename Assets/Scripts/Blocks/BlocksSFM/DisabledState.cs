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

		AssignTransparentMaterial();
		DisableFullLineScripts();
		DisableBoxColliders();
	}
	private void AssignTransparentMaterial()
	{
		foreach (var renderer in FSMBase.GetComponentsInChildren<Renderer>())
		{
			List<Material> newMatirials = new();

			foreach (var mat in renderer.sharedMaterials)
			{
				Material newMat = new Material(csm.TransparentMaterial);

				newMat.color = new Color(
					Mathf.Clamp01(mat.color.r + csm.GhostWhitness),
					Mathf.Clamp01(mat.color.g + csm.GhostWhitness),
					Mathf.Clamp01(mat.color.b + csm.GhostWhitness),
					csm.GhostTransparency
				);

				newMatirials.Add(newMat);
			}

			renderer.sharedMaterials = newMatirials.ToArray();
		}
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
