using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collision : MonoBehaviour
{
	private static readonly Dictionary<string, List<Collision>> collisionScripts = new();

	[SerializeField] private GameObject parent;
	private BoxCollider col;

	private bool isWallRight = false;
	private Ray BottomRay
	{
		get => new(transform.TransformPoint(col.center), Vector3.down);
	}
	private Ray[] RaysToCast
	{
		get
		{
			Vector3 worldSpaceColOrigin = transform.TransformPoint(col.center);

			return new Ray[]
			{
			new Ray(worldSpaceColOrigin, transform.up),
			new Ray(worldSpaceColOrigin, transform.right),
			new Ray(worldSpaceColOrigin, -transform.up),
			new Ray(worldSpaceColOrigin, -transform.right)
			};
		}
	}
	private void Awake()
	{
		col = GetComponent<BoxCollider>();
	}
	private void Start()
	{
		AddToList();
	}
	private void Update()
	{
		if (Time.timeScale == 0) return;
		DrawRays();
	}
	private void OnDestroy()
	{
		collisionScripts[parent.name].Remove(this);
	}
	//Moje metody
	#region static
	/// <summary>
	/// Tells wether the block will collide with the wall after the next horizontal move
	/// </summary>
	/// <param name="parentName">The parent name of cubes used for identifing the cubes</param>
	/// <returns>CollisionResult struct with the bool information and the direction of future possible collision</returns>
	public static CollisionResult IsNextToWall(string parentName)
	{
		if (!collisionScripts.ContainsKey(parentName)) return new CollisionResult(false, false);
		foreach (Collision collision in collisionScripts[parentName].Where(collision => collision.enabled).ToArray())
		{
			//Debug.Log("Wall: " + collision.isNextToWallCube);
			if (collision.WallCheck()) 
			{
				return new CollisionResult(true, collision.isWallRight);
			}
		}
		return new CollisionResult(false, false);
	}
	/// <summary>
	/// Tells wether the block will collide with the floor after the next verticall move
	/// </summary>
	/// <param name="parentName">The parent name of cubes used for identifing the cubes</param>
	public static bool IsNextToFloor(string parentName)
	{
		if(!collisionScripts.ContainsKey(parentName)) return false;
		foreach (Collision collision in collisionScripts[parentName].Where(collision => collision.enabled).ToArray())
		{
			//Debug.Log("Floor: " + collision.isNextToWallCube);
			if (collision.FloorCheck())
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Gets the closest point under the block
	/// Primarly used for hard drop
	/// </summary>
	/// <param name="parent">The parent name of cubes used for identifing the cubes</param>
	/// <returns>Vector3 with the position ready for hard drop</returns>
	public static Vector3 GetClosestBottomPoint(GameObject parent)
	{ 
		Collision finalCollision = collisionScripts[parent.name][0];
		RaycastHit finalHit = finalCollision.GetBottomPoint();
		float rayLength = finalHit.distance;
	
		foreach (Collision collision in collisionScripts[parent.name])
		{
			RaycastHit hit = collision.GetBottomPoint();
			if (hit.distance < rayLength)
			{
				finalCollision = collision;
				finalHit = hit;
				rayLength = hit.distance;
			}
		}
		float distanceToParent = parent.transform.position.y - finalCollision.transform.TransformPoint(finalCollision.col.center).y - 0.5f;
		return new Vector3(parent.transform.position.x, finalHit.point.y + finalCollision.col.size.y * 0.5f + distanceToParent, parent.transform.position.z);
	}
	#endregion
	private void AddToList()
	{
		if (!collisionScripts.ContainsKey(parent.name))
			collisionScripts[parent.name] = new List<Collision>();

		collisionScripts[parent.name].Add(this);
	}
	private bool WallCheck()
	{
		foreach (Ray ray in RaysToCast)
		{
			float dotRight = Vector3.Dot(ray.direction, Vector3.right);
			float dotLeft = Vector3.Dot(ray.direction, Vector3.left);

			if (dotRight != 1 && dotLeft != 1) continue;

			if (Physics.Raycast(ray, out RaycastHit hit, 0.2f + col.size.x * 0.25f, GameManager.Instance.WallMask))
			{
				//Debug.Log("Wall true");
				isWallRight = dotRight == 1;
				return true;
			}
		}
		return false;
	}
	private bool FloorCheck()
	{
		foreach (Ray ray in RaysToCast)
		{
			if (Vector3.Dot(Vector3.down, ray.direction) != 1) continue;

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 0.1f + col.size.y * 0.25f, GameManager.Instance.FloorMask))
			{
				//Debug.Log(parent.name + " Floor true");
				return true;
			}
		}
		return false;
	}
	private RaycastHit GetBottomPoint()
	{
		Physics.Raycast(BottomRay, out RaycastHit hit, Mathf.Infinity, GameManager.Instance.FloorMask);
		return hit;
	}
	private void DrawRays()
	{
		foreach (Ray ray in RaysToCast)
		{
			if (Vector3.Dot(ray.direction, Vector3.right) != 1 &&
				Vector3.Dot(ray.direction, Vector3.left) != 1) continue;

			Debug.DrawRay(ray.origin, ray.direction * (0.2f + (col.size.y * 0.25f)), Color.green);
		}

		foreach (Ray ray in RaysToCast)
		{
			if (Vector3.Dot(Vector3.down, ray.direction) != 1) continue;

			Debug.DrawRay(ray.origin, ray.direction * (0.1f + (col.size.y * 0.25f)), Color.red);
		}

		//Debug.DrawRay(BottomRay.origin, Vector3.down * 100);
	}
}