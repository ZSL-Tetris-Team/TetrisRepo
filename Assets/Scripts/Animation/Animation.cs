using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public static class Animation
{
	public static IEnumerator BreakLineAnimation(GameObject[] cubes, Action doAfter)
	{
		AudioEffectPlayer.Instance.PlayAudioEffect(GameManager.Instance.SfxManager.ChargeSound);
		for (float i = 1; i <= 1.4f; i += 0.025f)
		{
			foreach (GameObject cube in cubes)
			{
				Renderer renderer = cube.GetComponent<Renderer>();
				Material material = renderer.materials[1];
				Color emissionColor = material.GetColor("_EmissionColor");
				float intensity = i;

				material.SetColor("_EmissionColor", new Color(emissionColor.r, emissionColor.g, emissionColor.b) * intensity);
			}

			yield return new WaitForSeconds(0.04f);
		}

		AudioEffectPlayer.Instance.PlayAudioEffect(GameManager.Instance.SfxManager.LineBlowSound);

		foreach (GameObject cube in cubes)
		{
			CameraShake.Instance.ShakeCamera(33, 2, 0.3f);

			Material mat = cube.GetComponent<Renderer>().materials[1];
			Particles.Instance.PlayParticleOnce(Particles.ParticlesToUse.BlowCubeParticle, cube.transform.position, cube.transform.rotation, new Vector3(1, 1, 1), mat.GetColor("_EmissionColor"), 7);
		}

		doAfter();
	}
}
