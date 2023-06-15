using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingState : BlockState
{
	private BlockFSMBase FSMBase;

	private Timer timer;
	private InputManager inputManager;
	private ConstSettingsManager csm;
	private BoxCollider col;
	private Transform trans;
	private GameObject gameObject;
	private float horizontalDistanceToTravelRight = 0;
	private float horizontalDistanceToTravelLeft = 0;
	private float verticalDistanceToTravel = 0;
	private float hardDropDistanceToTravel = 0;
	private float horizontalSpeed;
	private float hardDropSpeed;
	public override void Start(BlockFSMBase b)
	{
		Debug.Log(b.gameObject.name + ": FallingState");

		FSMBase = b;

		timer = Timer.NewInstance;
		inputManager = b.InputManager;
		csm = b.Csm;
		col = b.Col;
		trans = b.transform;
		gameObject = b.gameObject;

		horizontalSpeed = csm.HorizontalBlockSpeed;
		hardDropSpeed = csm.HardDropBlockSpeed;

		EventManager.Instance.OnBlockFloorCollision.AddListener(SwitchToWaiting);

		DisableCubesColliders();
		b.GetComponent<BoxCollider>().enabled = true;

		//b.AudioSource.clip = b.spawnSound;
		//b.AudioSource.Play();

		VerticalMovement();
	}
	public override void Update(BlockFSMBase b)
	{
		HorizontalMovement();
		RotationalMovement();
		HandleSoftDrop();
		HandleHardDrop();
		VerticalMovementCollision();
	}
	public override void FixedUpdate(BlockFSMBase b)
	{
		//Distance to travel is used to make the smooth movement effect
		HandleDistanceToTravel();
	}
	public override void Exit(BlockFSMBase b)
	{
		EventManager.Instance.OnBlockFloorCollision.RemoveListener(SwitchToWaiting);
	}
	public override void OnDestroy(BlockFSMBase b)
	{
		EventManager.Instance.OnBlockFloorCollision.RemoveListener(SwitchToWaiting);
	}
	//This makes sure that the block will not clip through the wall if the move key has been spammed
	public override void OnCollisionEnter(UnityEngine.Collision collision)
	{
		horizontalDistanceToTravelLeft = 0;
		horizontalDistanceToTravelRight = 0;

		trans.position = new Vector3((float)Math.Round(trans.position.x), trans.position.y, trans.position.z);
	}
	private void SwitchToWaiting()
	{
		FSMBase.SwitchState(FSMBase.WaitingState);
	}
	private void DisableCubesColliders()
	{
		foreach(var col in FSMBase.GetComponentsInChildren<BoxCollider>())
		{
			col.enabled = false;
		}
	}
	private void VerticalMovement()
	{
		verticalDistanceToTravel++;

		float timeLeft = inputManager.GetSoftDropHold() ? csm.SoftDropFallTime : csm.FallTime;
		timer.TimeLeft = timeLeft;

		//Uruchamiam timer i podajê metode do wywo³ania po zakoñczeniu odliczania
		timer.StartTimer(VerticalMovement);
	}
	private void HorizontalMovement()
	{
		if (!inputManager.GetRight() && !inputManager.GetLeft()) return;

		//Sprawdzam czy naciœniêty jest prawy przycisk i czy blok nie jest obok prawej œciany
		CollisionResult colResult = Collision.IsNextToWall(gameObject.name);
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
			trans.Rotate(new Vector3(0, 0, -90));
			RotateParticles(90);

			if (WallClipping.IsWallClipping(gameObject.name))
			{
				trans.Rotate(new Vector3(0, 0, 90));
				RotateParticles(-90);
			}
		}
		if (inputManager.GetRotateLeft())
		{
			trans.Rotate(new Vector3(0, 0, 90));
			RotateParticles(-90);

			if (WallClipping.IsWallClipping(gameObject.name))
			{
				trans.Rotate(new Vector3(0, 0, -90));
				RotateParticles(90);
			}
		}
	}
	private void RotateParticles(float degrees)
	{
		foreach(Transform cube in FSMBase.transform)
		{
			Transform burstParticle = cube.GetChild(0);
			burstParticle.RotateAround(cube.position, Vector3.forward, degrees);
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
			timer.ResetTimer();
			verticalDistanceToTravel = verticalDistanceToTravel % 1;
			timer.TimeLeft = csm.FallTime;
			timer.StartTimer(VerticalMovement);
		}
	}
	private void HandleHardDrop()
	{
		if (inputManager.GetHardDrop())
		{
			trans.position = Collision.GetClosestBottomPoint(gameObject);
			HandleParticles();
			EventManager.Instance.OnBlockFloorCollision.Invoke();
		}
	}
	private void HandleParticles()
	{
		foreach(GameObject cube in Collision.GetScriptsByName(gameObject.name))
		{
			if (cube.GetComponent<Collision>().FloorCheck())
			{
				SetParticlesColor(cube);
				PlayParticles(cube);
			}
		}
	}
	private void SetParticlesColor(GameObject cube)
	{
		Renderer renderer = cube.GetComponent<Renderer>();
		ParticleSystem ps = cube.transform.GetChild(0).GetComponent<ParticleSystem>();
		var main = ps.main;
		main.startColor = new ParticleSystem.MinMaxGradient(renderer.materials[1].GetColor("_EmissionColor"));
	}
	private void PlayParticles(GameObject cube)
	{
		cube.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
	}
	private void VerticalMovementCollision()
	{
		if (!FSMBase.enabled) return;
		if (Collision.IsNextToFloor(gameObject.name) && horizontalDistanceToTravelRight <= 0 && horizontalDistanceToTravelLeft <= 0)
		{
			trans.position = new Vector3(trans.position.x, (float)Math.Round(trans.position.y), trans.position.z);
			Debug.Log(gameObject.name + ": onfloorcollision");
			EventManager.Instance.OnBlockFloorCollision.Invoke();
		}
	}
	private void HandleDistanceToTravel()
	{
		if (horizontalDistanceToTravelRight > 0)
		{
			trans.position += new Vector3(horizontalSpeed, 0, 0);
			horizontalDistanceToTravelRight -= horizontalSpeed;
		}
		if (horizontalDistanceToTravelLeft > 0)
		{
			trans.position += new Vector3(-horizontalSpeed, 0, 0);
			horizontalDistanceToTravelLeft -= horizontalSpeed;
		}
		if (verticalDistanceToTravel > 0)
		{
			trans.position += new Vector3(0, -horizontalSpeed, 0);
			verticalDistanceToTravel -= horizontalSpeed;
		}
		if (horizontalDistanceToTravelLeft < 0)
		{
			horizontalDistanceToTravelLeft = 0;
			trans.position = new Vector3((float)Math.Round(trans.position.x), trans.position.y, trans.position.z);
		}
		if (horizontalDistanceToTravelRight < 0)
		{
			horizontalDistanceToTravelRight = 0;
			trans.position = new Vector3((float)Math.Round(trans.position.x), trans.position.y, trans.position.z);
		}
	}
}
