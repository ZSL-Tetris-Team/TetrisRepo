using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksMovement : MonoBehaviour
{
    //Pola klasy
    private Timer timer;
    private InputManager inputManager;
	private ConstSettingsManager csm;

    //Metoda Awake jest dziedziczona z MonoBehaviour i jest wywo�ywana raz po za�adowaniu instancji tej klasy czyli po ztworzeniu GameObjectu z
    //tym skryptem
    private void Awake()
	{
        //Cachuje instancje InputManagera bo nie chce si� pisa�, Instance to moje property, zapraszam do klasy InputManager
        timer = Timer.NewInstance;
        inputManager = InputManager.Instance;
        csm = GameManager.Instance.ConstSettingsManager;
		VerticalMovement();
		EventManager.OnBlockFloorCollision.AddListener(() => { enabled = false; });
	}
	//Metoda Update te� jest dziedziczona i jest wykonywana co ka�d� klatk�
	private void Update()
	{
		HorizontalMovement();
		RotationalMovement();
		HandleSoftDrop();
	}
	private void HorizontalMovement()
	{
		CollisionResult colResult = Collision.IsNextToWallObject(gameObject.name);
		//Sprawdzam czy naci�ni�ty jest prawy przycisk i czy blok nie jest obok prawej �ciany
		bool collisionBoolRight = (colResult.IsColliding && !colResult.IsWallRight) || (!colResult.IsColliding);
		bool collisionsBoolLeft = (colResult.IsColliding && colResult.IsWallRight) || (!colResult.IsColliding);
		if (inputManager.GetRight() && collisionBoolRight)
		{
			transform.position += new Vector3(1, 0, 0);
		}
		if(inputManager.GetLeft() && collisionsBoolLeft)
		{
			transform.position += new Vector3(-1, 0, 0);
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
	private void VerticalMovement()
	{
        if (!enabled) return;
		
		transform.position -= new Vector3(0, 1, 0);

        float timeLeft = inputManager.GetSoftDropHold() ? csm.SoftDropFallTime : csm.FallTime;
        timer.TimeLeft = timeLeft;
		//Uruchamiam timer i podaj� metode do wywo�ania po zako�czeniu odliczania
		timer.StartTimer(VerticalMovement);
	}
}
