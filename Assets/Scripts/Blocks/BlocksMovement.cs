using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class BlocksMovement : MonoBehaviour
{
	//Pola klasy
	private Timer timer;
	private InputManager inputManager;
	private ConstSettingsManager csm;
	private float horizontalDistanceToTravel = 0;
	private float verticalDistanceToTravel = 0;
	private float horizontalSpeed;

	//Metoda Awake jest dziedziczona z MonoBehaviour i jest wywo�ywana raz po za�adowaniu instancji tej klasy czyli po ztworzeniu GameObjectu z
	//tym skryptem
	private void Awake()
	{
		//Cachuje instancje InputManagera bo nie chce si� pisa�, Instance to moje property, zapraszam do klasy InputManager
		timer = Timer.NewInstance;
		inputManager = InputManager.Instance;
		csm = GameManager.Instance.ConstSettingsManager;
		horizontalSpeed = csm.HorizontalBlockSpeed;
		EventManager.OnBlockFloorCollision.AddListener(() => { enabled = false; });
		VerticalMovement();
	}
	//Metoda Update te� jest dziedziczona i jest wykonywana co ka�d� klatk�
	private void Update()
	{
		HorizontalMovement();
		RotationalMovement();
		HandleSoftDrop();
		HandleHardDrop();
		HandleDistanceToTravel();
	}
	private void HandleDistanceToTravel()
	{
		if (horizontalDistanceToTravel > 0)
		{
			transform.position += new Vector3(Time.deltaTime * horizontalSpeed, 0, 0);
			horizontalDistanceToTravel -= Time.deltaTime * horizontalSpeed;
		}
		if (horizontalDistanceToTravel < 0)
		{
			transform.position += new Vector3(-Time.deltaTime * horizontalSpeed, 0, 0);
			horizontalDistanceToTravel += Time.deltaTime * horizontalSpeed;
		}
		if(verticalDistanceToTravel > 0)
		{
			transform.position += new Vector3(0, -Time.deltaTime * horizontalSpeed, 0);
			verticalDistanceToTravel -= Time.deltaTime * horizontalSpeed;
		}
	}
	private void HorizontalMovement()
	{
		if (!inputManager.GetRight() && !inputManager.GetLeft()) return;
		
		CollisionResult colResult = Collision.IsNextToWall(gameObject.name);
		//Sprawdzam czy naci�ni�ty jest prawy przycisk i czy blok nie jest obok prawej �ciany
		bool collisionBoolRight = (colResult.IsColliding && !colResult.IsWallRight) || (!colResult.IsColliding);
		bool collisionsBoolLeft = (colResult.IsColliding && colResult.IsWallRight) || (!colResult.IsColliding);
		if (inputManager.GetRight() && collisionBoolRight)
		{
			horizontalDistanceToTravel++;
		}
		if (inputManager.GetLeft() && collisionsBoolLeft)
		{
			horizontalDistanceToTravel--;
		}
	}
	private void RotationalMovement()
	{
		if (inputManager.GetRotateRight())
		{
			transform.Rotate(new Vector3(0, 0, -90));
		}
		if (inputManager.GetRotateLeft())
		{
			transform.Rotate(new Vector3(0, 0, 90));
		}
	}
	private void HandleSoftDrop()
	{
		if (inputManager.GetSoftDropDown())
		{
			timer.TimeLeft = 0;
		}
		if (inputManager.GetSoftDropUp())
		{
			timer.TimeLeft = csm.FallTime * 0.5f;
		}
	}
	private void HandleHardDrop()
	{
		if (inputManager.GetHardDrop())
		{
			transform.position = Collision.GetClosestBottomPoint(gameObject);
			EventManager.OnBlockFloorCollision.Invoke();
        }
	}
	private void VerticalMovement()
	{
		if (!enabled) return;
		if (Collision.IsNextToFloor(gameObject.name))
		{
			EventManager.OnBlockFloorCollision.Invoke();
		}

		verticalDistanceToTravel++;

		float timeLeft = inputManager.GetSoftDropHold() ? csm.SoftDropFallTime : csm.FallTime;
		timer.TimeLeft = timeLeft;

		//Uruchamiam timer i podaj� metode do wywo�ania po zako�czeniu odliczania
		timer.StartTimer(VerticalMovement);
	}
}