using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    //To jest dwu wymiarowe dictionary?
    //Bêdzie ono przechowywa³o wszystkie instancje klasy Collisions
    //Dictionary w c++ to mapa
    //Klucz (string) to nazwa rodzica tego skryptu, z niej dostajemy listê skryptów nale¿¹cych do dzieci rodzica
    //Ka¿dy indeks w liœcie odpowiada id skryptu Collision
    //Na koniec dostajemy sam¹ instancjê
    private static int collisionsCount = 0;
    private static Dictionary<string, List<Collision>> collisionsScripts = new();
    
    [SerializeField] private GameObject parent;
    private Ray[] raysToCast;
    private Ray bottomRay;
    private bool isNextToWallCube = false;
    private bool isNextToFloorCube = false;
    private BoxCollider col;

    public int Id { get; private set; }
    public bool IsWallRight { get; private set; } = false;

    //Awake jest wywo³aywany podczas ³adowania tej instancji klasy
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = false;
        Id = collisionsCount++;
        AddToList();
        raysToCast = GetUpdatedRays();
        EventManager.OnBlockFloorCollision.AddListener(DisableAllCollisions);
    }
    //Update jest wywo³ywany co ka¿d¹ klatkê
    private void Update()
    {
        raysToCast = GetUpdatedRays();
        DrawRays();
    }
    private void FixedUpdate()
    {
        isNextToWallCube = WallCheck();
        isNextToFloorCube = FloorCheck();
        InvokeEvents();
    }
    //Moje metody
    public static CollisionResult IsNextToWallObject(string parentName)
    {
        foreach (Collision collision in collisionsScripts[parentName])
        {
            //Debug.Log("Wall: " + collision.isNextToWallCube);
            if (collision.isNextToWallCube) return new CollisionResult(true, collision.IsWallRight);
        }
        return new CollisionResult(false, false);
    }
    public static bool IsNextToFloorObject(string parentName)
    {
        foreach (Collision collision in collisionsScripts[parentName])
        {
            //Debug.Log("Floor: " + collision.isNextToWallCube);
            if (collision.isNextToFloorCube) return true;
        }
        return false;
    }
    public static Vector3 GetClosestBottomPoint(GameObject parent)
    {
        Collision finalCollision = collisionsScripts[parent.name][0];
        float rayLength = finalCollision.GetBottomPoint().distance;

        foreach(Collision collision in collisionsScripts[parent.name])
        {
            RaycastHit hit = collision.GetBottomPoint();
            if (hit.distance < rayLength)
            {
                rayLength = hit.distance;
                finalCollision = collision;
            }
        }

        return new Vector3(parent.transform.position.x, finalCollision.GetBottomPoint().point.y, parent.transform.position.z);
    }
    private void AddToList()
    {
        if (!collisionsScripts.ContainsKey(parent.name))
            collisionsScripts[parent.name] = new List<Collision>();

        collisionsScripts[parent.name].Add(this);
    }
    private bool WallCheck()
    {
        foreach (Ray ray in raysToCast)
        {
            float dotRight = Vector3.Dot(ray.direction, Vector3.right);
            float dotLeft = Vector3.Dot(ray.direction, Vector3.left);

            if (dotRight != 1 && dotLeft != 1) continue;

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 0.9f, GameManager.Instance.WallMask))
            {
                Debug.Log("Wall true");
                IsWallRight = dotRight == 1;
                return true;
            }
        }
        return false;
    }
    private bool FloorCheck()
    {
        foreach (Ray ray in raysToCast)
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
        bottomRay = GetUpdatedBottomRay();
        RaycastHit hit;
        Physics.Raycast(bottomRay, out hit, Mathf.Infinity, GameManager.Instance.FloorMask);
        return hit;
    }
    private void DrawRays()
    {
        foreach(Ray ray in raysToCast)
        {
            if (Vector3.Dot(ray.direction, Vector3.right) != 1 &&
                Vector3.Dot(ray.direction, Vector3.left) != 1) continue;
            
            Debug.DrawRay(ray.origin, ray.direction * 0.9f, Color.green);
        }

        foreach (Ray ray in raysToCast)
        {
            if (Vector3.Dot(Vector3.down, ray.direction) != 1) continue;

            Debug.DrawRay(ray.origin, ray.direction * 0.9f, Color.red);
        }

        bottomRay = GetUpdatedBottomRay();
        Debug.DrawRay(bottomRay.origin, Vector3.down * 100);
    }
    private Ray[] GetUpdatedRays()
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
    private Ray GetUpdatedBottomRay()
    {
        return new Ray(transform.TransformPoint(col.center), Vector3.down);
    }
    private void InvokeEvents()
    {
        if(isNextToFloorCube)
        {
            EventManager.OnBlockFloorCollision.Invoke();
            EventManager.SwitchGameManagerState.Invoke(GameManager.States.InstantiateBlock);
        }
    }
    private void DisableAllCollisions()
    {
        foreach (Collision collision in collisionsScripts[parent.name])
        {
            collision.enabled = false;
            collision.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
