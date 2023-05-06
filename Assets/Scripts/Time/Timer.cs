using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //Moje statyczne property - czyli zar�wno gettery i settery w jednym
    //Zwraca one za ka�dym razem now� instancje Timer
    //Wygl�da to tak dziwnie bo Timer dziedziczy po MonoBehaviour, czyli jest komponentem, czyli musi by� przypisany do obiektu GameObject
    //a do czego potrzebuj� MoboBahavoiur? Do metody Update kt�ra co klatk� b�dzie wykonywa�a odliczanie
    public static Timer NewInstance
    {
        get
        {
            GameObject obj = new();
            obj.name = typeof(Timer).Name;
            //Dzi�ki temu obiekt jest nie widzoczny i nie b�dzie zapisany czy co�
            obj.hideFlags = HideFlags.HideAndDontSave;
            //Dodaj� do GameObject komponent Timer (instancj�) i jednocze�nie j� zwracam
            return obj.AddComponent<Timer>();
        }
    }

    private bool isActive = false;
	private float _timeLeft;
	private int _timeLeftInt;
    //To jest wbudowany delegat Action
    //Delegaty s� takim wska�nikiem do funkcji
    //s� one mi potrzebne aby wywo�a� funkcj� podan� w argumencie metody Start
	private Action functionToCall;
    //Takie fiku�ne property (gettery i settery)
	public float TimeLeft 
    {
        get => _timeLeft;
        set { 
            _timeLeft = value; 
            _timeLeftInt = Mathf.FloorToInt(_timeLeft);
        }
    }
    //Property bez settera
    public int TimeLeftInt 
    { 
        get => _timeLeftInt;
        private set => _timeLeftInt = value;
    }
	private void Update()
	{
        //Sprawdzam czy timer jest atywny
        if (!isActive) return;
        //Odejmuj� warto�c deltaTime od pozosta�ego czasu
        //DeltaTime to czas kt�ry up�yn�� mi�czy poprzedni� a t� klatk�
		TimeLeft -= Time.deltaTime;
        if(TimeLeft < 0)
        {
            StopTimer();
            //Wywo�uj� metod� do wywo�ania przez delegat functionToCall
            functionToCall();
        }
	}
    public void StartTimer()
    {
        isActive = true;
    }
    public void StartTimer(Action functionToCall)
	{
        //This jest wska�nikiem, ale �e w c# nie ma defakto poj�cia wska�nik to i tak urzywamy kropki
        //Zreszt� do statycznych rzeczy te� kropeczka
        this.functionToCall = functionToCall;
		isActive = true;
	}
    public void StopTimer()
    {
        isActive = false;
    }
    public void ResetTimer()
    {
        isActive = false;
        TimeLeft = 0;
        functionToCall = null;
    }
    public void ToggleTimer()
    {
        isActive = !isActive;
    }
}
