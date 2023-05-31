using UnityEngine;
using System.Collections;

public class TerrainBehaviour : MonoBehaviour
{
	private Terrain terrain;

	[SerializeField] private Transform leavesPlaceHolder;
	[SerializeField]private ParticleSystem leaves;
	[SerializeField]private float heightOffset = 8f;

	void Start()
	{
		terrain = Terrain.activeTerrain;
		Vector3 terrainSize = terrain.terrainData.size;

		for(int i = 0; i < terrain.terrainData.treeInstances.Length; i++)
		{
			Vector3 pos = terrain.terrainData.treeInstances[i].position;
			ParticleSystem particles = Instantiate(leaves);
			particles.transform.position = new Vector3(pos.x*terrainSize.x, pos.y*terrainSize.y+heightOffset, pos.z*terrainSize.z);
			particles.transform.parent = leavesPlaceHolder;
		}
	}
}
