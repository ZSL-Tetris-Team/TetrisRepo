using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlocksMovement : MonoBehaviour
{
	//Pola klasy
	private Timer timer;
	private InputManager inputManager;
	private ConstSettingsManager csm;
	private float horizontalDistanceToTravelRight = 0;
	private float horizontalDistanceToTravelLeft = 0;
	private float verticalDistanceToTravel = 0;
	private float hardDropDistanceToTravel = 0;
	private float horizontalSpeed;
	private float hardDripSpeed;

	//Metoda Awake jest dziedziczona z MonoBehaviour i jest wywo³ywana raz po za³adowaniu instancji tej klasy czyli po ztworzeniu GameObjectu z
	//tym skryptem
	private void Awake()
	{
		//Cachuje instancje InputManagera bo nie chce siê pisaæ, Instance to moje property, zapraszam do klasy InputManager
		timer = Timer.NewInstance;
		inputManager = InputManager.Instance;
		csm = GameManager.Instance.ConstSettingsManager;
		horizontalSpeed = csm.HorizontalBlockSpeed;
		hardDripSpeed = csm.HardDropBlockSpeed;
		EventManager.OnBlockFloorCollision.AddListener(Disable);
		//EventManager.OnCubeFall.AddListener(OnCubeFall);
	}
	private void Start()
	{
		VerticalMovement();
	}
	//Metoda Update te¿ jest dziedziczona i jest wykonywana co ka¿d¹ klatkê
	private void Update()
	{
		HorizontalMovement();
		RotationalMovement();
		HandleSoftDrop();
		HandleHardDrop();
		HandleDistanceToTravel();
		VerticalMovementCollision();
	}
	private void OnDestroy()
	{
		EventManager.OnBlockFloorCollision.RemoveListener(Disable);
		timer.ResetTimer();
	}
	private void Disable()
	{
		enabled = false;
	}
	private void HandleDistanceToTravel()
	{
		if (horizontalDistanceToTravelRight > 0)
		{
			transform.position += new Vector3(Time.deltaTime * horizontalSpeed, 0, 0);
			horizontalDistanceToTravelRight -= Time.deltaTime * horizontalSpeed;
		}
		if (horizontalDistanceToTravelLeft > 0)
		{
			transform.position += new Vector3(-Time.deltaTime * horizontalSpeed, 0, 0);
			horizontalDistanceToTravelLeft -= Time.deltaTime * horizontalSpeed;
		}
		if(verticalDistanceToTravel > 0)
		{
			transform.position += new Vector3(0, -Time.deltaTime * horizontalSpeed, 0);
			verticalDistanceToTravel -= Time.deltaTime * horizontalSpeed;
		}
		if (horizontalDistanceToTravelLeft < 0)
		{
			horizontalDistanceToTravelLeft = 0;
			transform.position = new Vector3((float)Math.Round(transform.position.x), transform.position.y, transform.position.z);
		}
		if (horizontalDistanceToTravelRight < 0)
		{
			horizontalDistanceToTravelRight = 0;
			transform.position = new Vector3((float)Math.Round(transform.position.x), transform.position.y, transform.position.z);
		}
		//if (hardDropDistanceToTravel > 0)
		//{
		//	transform.position += new Vector3(0, -Time.deltaTime * horizontalSpeed, 0);
		//	hardDropDistanceToTravel -= Time.deltaTime * horizontalSpeed;
		//}
	}
	public void OnCubeFall()
	{
		Debug.Log("down");
		verticalDistanceToTravel++;
	}
	private void HorizontalMovement()
	{
		if (!inputManager.GetRight() && !inputManager.GetLeft()) return;
		
		CollisionResult colResult = Collision.IsNextToWall(gameObject.name);
		//Sprawdzam czy naciœniêty jest prawy przycisk i czy blok nie jest obok prawej œciany
		bool collisionBoolRight = (colResult.IsColliding && !colResult.IsWallRight) || (!colResult.IsColliding);
		bool collisionsBoolLeft = (colResult.IsColliding && colResult.IsWallRight) || (!colResult.IsColliding);

		if (inputManager.GetRight() && collisionBoolRight)
		{
			horizontalDistanceToTravelRight++;
		}
		if (inputManager.GetLeft() && collisionsBoolLeft)
		{
			horizontalDistanceToTravelLeft++;
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
			//hardDropDistanceToTravel += Mathf.Abs(Collision.GetClosestBottomPoint(gameObject).y - transform.position.y) - 1;
			transform.position = Collision.GetClosestBottomPoint(gameObject);
			EventManager.OnBlockFloorCollision.Invoke();
		}
	}
	private void VerticalMovementCollision()
	{
		if (Collision.IsNextToFloor(gameObject.name) && verticalDistanceToTravel <= 0 && horizontalDistanceToTravelRight <= 0 && horizontalDistanceToTravelLeft <= 0)
		{
			EventManager.OnBlockFloorCollision.Invoke();
		}
	}
	private void VerticalMovement()
	{
		if (!enabled) return;
		verticalDistanceToTravel++;

		float timeLeft = inputManager.GetSoftDropHold() ? csm.SoftDropFallTime : csm.FallTime;
		timer.TimeLeft = timeLeft;

		//Uruchamiam timer i podajê metode do wywo³ania po zakoñczeniu odliczania
		timer.StartTimer(VerticalMovement);
	}
}