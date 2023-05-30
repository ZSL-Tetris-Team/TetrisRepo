using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singelton<InputManager>
{
	//Ta klasa dziedziczy po Singeltonie z generycznoœci¹
	//Singelton to klasa która mo¿e mieæ tylko jedn¹ instancjê
	//U¿ywam tego teraz zamiast statycznej klasy po to aby mieæ dostêp do klasy MonoBehaviour (metoda Awake, OnEnable, OnDisable)
	//Singelton dziedziczy do monobehaviour czyli musi mieæ jak¹æ instancjê aby dzia³aæ, i tworzy tylko jedn¹
	//w efekcie dostajê klase z przyrównywalnym dostêpem co klasa statyczna i jednoczeœnie mam dostêp do MonoBehaviour
	private Input input;
	//Metoda Awake jest uruchamiana podczas tworzenia obiektu jej klasy (instancji)
	//jest ona pierwsz¹ wywo³ywan¹ metod¹ przed OnEnable i Start
	private void Awake()
	{
		//Tworzê obiekt klasy Input klasy automatycznie wygenerowanej przes Unity z InputActions s³u¿¹cego do customizacji inputu
		input = new();
	}
	private void OnEnable()
	{
		//Aktywujê obiekt
		input.Enable();
	}
	private void OnDisable()
	{
		input.Disable();
	}
	//Tutaj s¹ metody które umo¿liwiaj¹ mi dostêp do wartoœci poszczególnych inputów
	//Tego ¿eczywiœcie bêdê u¿ywaæ z zewn¹trz klasy
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

