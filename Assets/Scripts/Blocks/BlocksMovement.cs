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
	//metoda Start jest dziedziczona z MonoBehaviour i jest wywo�ywana po za�adowaniu wszystkiego zaraz przed uruchomieniem wszystkich system�w
	private void Start()
	{
		//pobieram komponent Transform (obiekt) od rodzica (GameObjectu) bo klasy dziedzicz�ce MonoBehoviour s� r�wnie� kompon�tami sk�adowymi owego GameObjectu (i innych)
		transform = GetComponent<Transform>();
		//cachuje instancje InputManagera bo nie chce si� pisa�, Instance to moje property, zapraszam do klasy InputManager
		inputManager = InputManager.Instance;
		VerticalMovement();
	}
	//metoda Update te� jest dziedziczona i jest wykonywana co ka�d� klatk�
	private void Update()
	{
		HorizontalMovement();
	}
	//moja metoda kt�ra ma za zadanie obs�ugiwa� poziomy movement
	private void HorizontalMovement()
	{
		//sprawdzam czy naci�ni�ty jest prawy przycisk po wi�cej informacii zapraszam do klasy inputManager
		if (inputManager.GetRight())
		{
			//ta linijka czyli metoda Translate dodaje wektor w argumencie do obecnego wektoru wyznaczaj�cego pozycje
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
		//Two�� now� instancje klasy Timer za pomoc� property, zapraszam do tej klasy
		timer = Timer.NewInstance;
		timer.TimeLeft = 1;
		//uruchamiam timer i podaj� metode do wywo�ania po zako�czeniu odliczania
		timer.StartTimer(VerticalMovement);
	}

}
