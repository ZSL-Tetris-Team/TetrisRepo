using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksMovement : MonoBehaviour
{
	//pola klasy
	private Transform transform;
	private InputManager inputManager;
	private Timer timer;
	//metody klasy
	//metoda Start jest dziedziczona z MonoBehaviour i jest wywo³ywana po za³adowaniu wszystkiego zaraz przed uruchomieniem wszystkich systemów
	private void Start()
	{
		//pobieram komponent Transform (obiekt) od rodzica (GameObjectu) bo klasy dziedzicz¹ce MonoBehoviour s¹ równie¿ komponêtami sk³adowymi owego GameObjectu (i innych)
		transform = GetComponent<Transform>();
		//cachuje instancje InputManagera bo nie chce siê pisaæ, Instance to moje property, zapraszam do klasy InputManager
		inputManager = InputManager.Instance;
		VerticalMovement();
	}
	//metoda Update te¿ jest dziedziczona i jest wykonywana co ka¿d¹ klatkê
	private void Update()
	{
		HorizontalMovement();
	}
	//moja metoda która ma za zadanie obs³ugiwaæ poziomy movement
	private void HorizontalMovement()
	{
		//sprawdzam czy naciœniêty jest prawy przycisk po wiêcej informacii zapraszam do klasy inputManager
		if (inputManager.GetRight())
		{
			//ta linijka czyli metoda Translate dodaje wektor w argumencie do obecnego wektoru wyznaczaj¹cego pozycje
			transform.Translate(Vector3.right);
		}
		if (inputManager.GetLeft())
		{
			transform.Translate(Vector3.left);
		}
	}
	private void VerticalMovement()
	{
		transform.Translate(Vector3.down);
		//Two¿ê now¹ instancje klasy Timer za pomoc¹ property, zapraszam do tej klasy
		timer = Timer.NewInstance;
		timer.TimeLeft = 1;
		//uruchamiam timer i podajê metode do wywo³ania po zakoñczeniu odliczania
		timer.StartTimer(VerticalMovement);
	}

}
