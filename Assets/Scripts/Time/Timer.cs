using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //Moje statyczne property - czyli zar�wno gettery i settery w jednym
    //zwraca one za ka�dym razem now� instancje Timer
    //wygl�da to tak dziwnie bo Timer dziedziczy po MonoBehaviour, czyli jest komponentem, czyli musi by� przypisany do obiektu GameObject
    //a do czego potrzebuj� MoboBahavoiur? Do metody Update kt�ra co klatk� b�dzie wykonywa�a odliczanie
    public static Timer NewInstance
    {
        get
        {
            GameObject obj = new();
            obj.name = typeof(Timer).Name;
            //dzi�ki temu obiekt jest nie widzoczny i nie b�dzie zapisany czy co�
            obj.hideFlags = HideFlags.HideAndDontSave;
            //dodaj� do GameObject komponent Timer (instancj�) i jednocze�nie j� zwracam
            return obj.AddComponent<Timer>();
        }
    }

    private bool isActive = false;
	private float _timeLeft;
	private int _timeLeftInt;
    //to jest wbudowany delegat Action
    //delegaty s� takim wska�nikiem do funkcji
    //jest on potrzebny aby wywo�a� funkcj� podan� w argumencie metody Start
	private Action functionToCall;
    //taki fiku�ne property (gettery i settery)
	public float TimeLeft 
    {
        get { return _timeLeft; }
        set { 
            _timeLeft = value; 
            _timeLeftInt = Mathf.FloorToInt(_timeLeft);
        }
    }
    //property bez settera
    public int TimeLeftInt { get { return _timeLeftInt; } }
	private void Update()
	{
        //sprawdzam czy timer jest atywny
        if (!isActive) return;
        //doejmuj� warto�c deltaTime od pozosta�ego czasu
        //deltaTime to czas kt�ry up�yn�� mi�czy poprzedni� a t� klatk�
		TimeLeft -= Time.deltaTime;
        if(TimeLeft < 0)
        {
            StopTimer();
            //wywo�uj� metod� do wywo�ania przez delegat functionToCall
            functionToCall();
        }
	}
	public void StartTimer(Action functionToCall)
	{
        //this jest wska�nikiem, ale �e w c# nie ma defakto poj�cia wska�nik to i tak urzywamy kropki
        //zreszt� do statycznych rzeczy te� kropeczka
        this.functionToCall = functionToCall;
		isActive = true;
	}
    public void StopTimer()
    {
        isActive = false;
    }
    public void ToggleTimer()
    {
        isActive = !isActive;
    }
}
