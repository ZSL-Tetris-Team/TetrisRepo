using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
	//To jest dwu wymiarowe dictionary?
	//Będzie ono przechowywało wszystkie instancje klasy Collisions
	//Dictionary w c++ to mapa
	//Klucz (string) to nazwa rodzica tego skryptu, z niej dostajemy listę skryptów należących do dzieci rodzica
	//Każdy indeks w liście odpowiada id skryptu Collision
	//Na koniec dostajemy sam¹ instancję
	private static readonly Dictionary<string, List<Collision>> collisionScripts = new();

	[SerializeField] private GameObject parent;
	private bool isNextToFloor = false;
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
	//Awake jest wywoływany podczas ładowania tej instancji klasy
	private void Awake()
	{
		col = GetComponent<BoxCollider>();
		col.enabled = false;

		EventManager.OnBlockFloorCollision.AddListener(DisableAllCollisions);
	}
	private void Start()
	{
		AddToList();
	}
	//Update jest wywo³ywany co każdą klatkę
	private void Update()
	{
		DrawRays();
	}
	private void FixedUpdate()
	{
		isNextToFloor = FloorCheck();
	}
	private void OnDestroy()
	{
		collisionScripts[parent.name].Remove(this);
	}
	//Moje metody
	#region static
	public static CollisionResult IsNextToWall(string parentName)
	{
		if (!collisionScripts.ContainsKey(parentName)) return new CollisionResult(false, false);
		foreach (Collision collision in collisionScripts[parentName])
		{
			//Debug.Log("Wall: " + collision.isNextToWallCube);
			if (collision.WallCheck()) return new CollisionResult(true, collision.isWallRight);
		}
		return new CollisionResult(false, false);
	}
	public static bool IsNextToFloor(string parentName)
	{
		if(!collisionScripts.ContainsKey(parentName)) return false;
		foreach (Collision collision in collisionScripts[parentName])
		{
			//Debug.Log("Floor: " + collision.isNextToWallCube);
			if (collision.isNextToFloor) return true;
		}
		return false;
	}
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
		float distanceToParent = parent.transform.position.y - finalCollision.transform.TransformPoint(finalCollision.col.center).y;
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

			if (Physics.Raycast(ray, out RaycastHit hit, 0.9f, GameManager.Instance.WallMask))
			{
				Debug.Log("Wall true");
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
			if (Physics.Raycast(ray, out hit, 0.9f, GameManager.Instance.FloorMask))
			{
				Debug.Log("Floor true");
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
	private void DisableAllCollisions()
	{
		foreach (Collision collision in collisionScripts[parent.name])
		{
			collision.enabled = false;
			collision.GetComponent<BoxCollider>().enabled = true;
		}
	}
	private void DrawRays()
	{
		foreach (Ray ray in RaysToCast)
		{
			if (Vector3.Dot(ray.direction, Vector3.right) != 1 &&
				Vector3.Dot(ray.direction, Vector3.left) != 1) continue;

			Debug.DrawRay(ray.origin, ray.direction * 0.9f, Color.green);
		}

		foreach (Ray ray in RaysToCast)
		{
			if (Vector3.Dot(Vector3.down, ray.direction) != 1) continue;

			Debug.DrawRay(ray.origin, ray.direction * 0.9f, Color.red);
		}

		Debug.DrawRay(BottomRay.origin, Vector3.down * 100);
	}
}