using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singelton<InputManager>
{
	//ta klasa dziedziczy po Singeltonie z generyczno�ci�
	//singelton to klasa kt�ra mo�e mie� tylko jedn� instancj�
	//urzywam tego teraz zamiast statycznej klasy po to aby mie� dost�p do klasy MonoBehaviour (metoda Awake, OnEnable, OnDisable)
	//singelton dziedziczy do monobehaviour czyli musi mie� jak�� instancj� aby dzia�a�, i tworzy tylko jedn�
	//w efekcie dostaj� klase z przyr�wnywalnym dost�pem co klasa statyczna i jednocze�nie mam dost�p do MonoBehaviour
	private Input _input;
	//metoda Awake jest uruchamiana podczas tworzenia obiektu jej klasy (instancji)
	//jest ona pierwsz� wywo�ywan� metod� przed OnEnable i Start
	private void Awake()
	{
		//tworz� obiekt klasy Input klasy automatycznie wygenerowanej przes Unity z InputActions s�u��cego do customizacji inputu
		_input = new();
	}
	private void OnEnable()
	{
		//aktyw�j� obiekt
		_input.Enable();
	}
	private void OnDisable()
	{
		_input.Disable();
	}
	//tutaj s� metody kt�re umo�liwiaj� mi dost�p do warto�ci poszczeg�lnych input�w
	//tego �eczywi�cie b�d� u�ywa� z zewn�trz klasy
	public bool GetRight()
	{
		return _input.Blocks.Right.triggered;
	}
	public bool GetLeft() 
	{
		return _input.Blocks.Left.triggered;
	}
}

