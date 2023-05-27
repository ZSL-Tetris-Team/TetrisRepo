using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledState : BlockState
{
	private GameManager gameManager;
	private ConstSettingsManager csm;
	public override void Start(BlockFSMBase b)
	{
		Debug.Log(b.gameObject.name + ": DisabledState");

		gameManager = b.GameManager;
		csm = gameManager.ConstSettingsManager;

		foreach (var renderer in b.GetComponentsInChildren<Renderer>())
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

		foreach (var lineHandler in b.GetComponentsInChildren<FullLineHandler>())
		{
			lineHandler.PermDisabled = true;
		}

		foreach (var col in b.GetComponentsInChildren<BoxCollider>())
		{
			col.enabled = false;
		}
	}
}
