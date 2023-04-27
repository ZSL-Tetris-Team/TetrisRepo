using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singelton<InputManager>
{
	//ta klasa dziedziczy po Singeltonie z generycznoœci¹
	//singelton to klasa która mo¿e mieæ tylko jedn¹ instancjê
	//urzywam tego teraz zamiast statycznej klasy po to aby mieæ dostêp do klasy MonoBehaviour (metoda Awake, OnEnable, OnDisable)
	//singelton dziedziczy do monobehaviour czyli musi mieæ jak¹æ instancjê aby dzia³aæ, i tworzy tylko jedn¹
	//w efekcie dostajê klase z przyrównywalnym dostêpem co klasa statyczna i jednoczeœnie mam dostêp do MonoBehaviour
	private Input _input;
	//metoda Awake jest uruchamiana podczas tworzenia obiektu jej klasy (instancji)
	//jest ona pierwsz¹ wywo³ywan¹ metod¹ przed OnEnable i Start
	private void Awake()
	{
		//tworzê obiekt klasy Input klasy automatycznie wygenerowanej przes Unity z InputActions s³u¿¹cego do customizacji inputu
		_input = new();
	}
	private void OnEnable()
	{
		//aktywójê obiekt
		_input.Enable();
	}
	private void OnDisable()
	{
		_input.Disable();
	}
	//tutaj s¹ metody które umo¿liwiaj¹ mi dostêp do wartoœci poszczególnych inputów
	//tego ¿eczywiœcie bêdê u¿ywaæ z zewn¹trz klasy
	public bool GetRight()
	{
		return _input.Blocks.Right.triggered;
	}
	public bool GetLeft() 
	{
		return _input.Blocks.Left.triggered;
	}
}

