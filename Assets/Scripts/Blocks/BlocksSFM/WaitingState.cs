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

		b.AudioSource.clip = b.fallenSound;
		b.AudioSource.Play();

		SetParticlesColor();
		PlayParticles();

		CameraShake.Instance.ShakeCamera(4, 2, 0.3f);
		Debug.Log("waiting");
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
	private void SetParticlesColor()
	{
		foreach (Transform cube in FSMBase.transform)
		{
			Renderer renderer = cube.GetComponent<Renderer>();
			ParticleSystem[] pss = cube.GetComponentsInChildren<ParticleSystem>();
			foreach(ParticleSystem ps in pss)
			{
				var main = ps.main;
				main.startColor = new ParticleSystem.MinMaxGradient(renderer.materials[1].GetColor("_EmissionColor"));
			}
		}
	}
	private void PlayParticles()
	{
		foreach(Transform cube in FSMBase.transform)
		{
			cube.GetComponentInChildren<ParticleSystem>().Play();
		}
	}
}

