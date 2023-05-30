using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singelton<InputManager>
{
	//Ta klasa dziedziczy po Singeltonie z generyczno�ci�
	//Singelton to klasa kt�ra mo�e mie� tylko jedn� instancj�
	//U�ywam tego teraz zamiast statycznej klasy po to aby mie� dost�p do klasy MonoBehaviour (metoda Awake, OnEnable, OnDisable)
	//Singelton dziedziczy do monobehaviour czyli musi mie� jak�� instancj� aby dzia�a�, i tworzy tylko jedn�
	//w efekcie dostaj� klase z przyr�wnywalnym dost�pem co klasa statyczna i jednocze�nie mam dost�p do MonoBehaviour
	private Input input;
	//Metoda Awake jest uruchamiana podczas tworzenia obiektu jej klasy (instancji)
	//jest ona pierwsz� wywo�ywan� metod� przed OnEnable i Start
	private void Awake()
	{
		//Tworz� obiekt klasy Input klasy automatycznie wygenerowanej przes Unity z InputActions s�u��cego do customizacji inputu
		input = new();
	}
	private void OnEnable()
	{
		//Aktywuj� obiekt
		input.Enable();
	}
	private void OnDisable()
	{
		input.Disable();
	}
	//Tutaj s� metody kt�re umo�liwiaj� mi dost�p do warto�ci poszczeg�lnych input�w
	//Tego �eczywi�cie b�d� u�ywa� z zewn�trz klasy
	public bool GetRight() => input.Blocks.Right.triggered;
	public bool GetLeft() => input.Blocks.Left.triggered;
	public bool GetRotateLeft() => input.Blocks.RotateLeft.triggered;
	public bool GetRotateRight() => input.Blocks.RotateRight.triggered;
	public bool GetSoftDropDown() => input.Blocks.SoftDropDown.triggered;
	public bool GetSoftDropHold() => input.Blocks.SoftDropHold.ReadValue<float>() > 0;
	public bool GetSoftDropUp() => input.Blocks.SoftDropUp.triggered;
	public bool GetHardDrop() => input.Blocks.HardDrop.triggered;
	public bool GetHoldPiece() => input.Blocks.HoldPiece.triggered;
	public bool GetPause() => input.Ui.Pause.triggered;
}

