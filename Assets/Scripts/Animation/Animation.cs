using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public static class Animation
{
	public static IEnumerator BreakLineAnimation(Renderer renderer, ParticleSystem ps)
	{
		for (float i = 1; i <= 1.4f; i += 0.025f)
		{
			Material material = renderer.materials[1];
			Color emissionColor = material.GetColor("_EmissionColor");
			float intensity = i;

			material.SetColor("_EmissionColor", new Color(emissionColor.r, emissionColor.g, emissionColor.b) * intensity);
			yield return new WaitForSeconds(0.04f);
		}

		CameraShake.Instance.ShakeCamera(20, 2, 0.3f);
		ps.Play();

		yield return new WaitForSeconds(1);
		EventManager.Instance.DestroyLineAnimationEnded.Invoke();
	}
}
