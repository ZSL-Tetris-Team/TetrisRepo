using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Particles : Singelton<Particles>
{
	public enum ParticlesToUse
	{
		BurstCubeParticle,
		BlowCubeParticle
	}
	private PrefabManager prefabManager;
	private GameObject burstCubeParticle;
	private GameObject blowCubeParticle;
	private void Awake()
	{
		prefabManager = GameManager.Instance.PrefabManager;
		burstCubeParticle = prefabManager.BurstCubeParticle;
		blowCubeParticle = prefabManager.BlowCubeParticle;
	}
	/// <summary>
	/// Creates and returns particle
	/// </summary>
	/// <returns>Created particle</returns>
	public GameObject CreateParticle(ParticlesToUse particle)
    {
		GameObject particleToCreate;
		switch (particle)
		{
			case ParticlesToUse.BurstCubeParticle:
				particleToCreate = burstCubeParticle;
				break;
			case ParticlesToUse.BlowCubeParticle:
				particleToCreate = blowCubeParticle;
				break;
			default:
				particleToCreate = blowCubeParticle;
				break;
		}

		GameObject instantiatedParticle = Instantiate(particleToCreate);

		return instantiatedParticle;
    }
	/// <summary>
	/// Creates, plays and delates particle.
	/// </summary>
	/// <param name="position">position of newly create gameobject</param>
	/// <param name="rotation">rotation of newly create gameobject</param>
	/// <param name="scale">scale of newly create gameobject</param>
	public void PlayParticleOnce(ParticlesToUse particle, Vector3 position, Quaternion rotation, Vector3 scale, float time = 1)
	{
		GameObject particleToCreate;
		switch (particle)
		{
			case ParticlesToUse.BurstCubeParticle:
				particleToCreate = burstCubeParticle;
				break;
			case ParticlesToUse.BlowCubeParticle:
				particleToCreate = blowCubeParticle;
				break;
			default:
				particleToCreate = blowCubeParticle;
				break;
		}

		GameObject instantiatedParticle = Instantiate(particleToCreate);
		instantiatedParticle.transform.position = position;
		instantiatedParticle.transform.rotation = rotation;
		instantiatedParticle.transform.localScale = scale;

		instantiatedParticle.GetComponentInParent<ParticleSystem>().Play();

		StartCoroutine(DestroyParticle(instantiatedParticle, time));
	}
	/// <summary>
	/// Creates, plays and delates particle.
	/// </summary>
	/// <param name="position">position of newly create gameobject</param>
	/// <param name="rotation">rotation of newly create gameobject</param>
	/// <param name="scale">scale of newly create gameobject</param>
	/// <param name="color">color of newly create gameobject</param>
	public void PlayParticleOnce(ParticlesToUse particle, Vector3 position, Quaternion rotation, Vector3 scale, Color color, float time = 1)
	{
		GameObject particleToCreate;
		switch (particle)
		{
			case ParticlesToUse.BurstCubeParticle:
				particleToCreate = burstCubeParticle;
				break;
			case ParticlesToUse.BlowCubeParticle:
				particleToCreate = blowCubeParticle;
				break;
			default:
				particleToCreate = blowCubeParticle;
				break;
		}

		GameObject instantiatedParticle = Instantiate(particleToCreate);
		instantiatedParticle.transform.position = position;
		instantiatedParticle.transform.rotation = rotation;
		instantiatedParticle.transform.localScale = scale;

		ParticleSystem ps = instantiatedParticle.GetComponentInParent<ParticleSystem>();

		var main = ps.main;
		main.startColor = color;
		ps.Play();

		StartCoroutine(DestroyParticle(instantiatedParticle, time));
	}
	private IEnumerator DestroyParticle(GameObject particle ,float time)
	{
		yield return new WaitForSeconds(time);

		Destroy(particle);
	}
	
}
