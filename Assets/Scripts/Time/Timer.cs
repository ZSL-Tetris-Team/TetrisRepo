using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //Moje statyczne property - czyli zarówno gettery i settery w jednym
    //Zwraca one za ka¿dym razem now¹ instancje Timer
    //Wygl¹da to tak dziwnie bo Timer dziedziczy po MonoBehaviour, czyli jest komponentem, czyli musi byæ przypisany do obiektu GameObject
    //a do czego potrzebujê MoboBahavoiur? Do metody Update która co klatkê bêdzie wykonywa³a odliczanie
    public static Timer NewInstance
    {
        get
        {
            GameObject obj = new();
            obj.name = typeof(Timer).Name;
            //Dziêki temu obiekt jest nie widzoczny i nie bêdzie zapisany czy coœ
            obj.hideFlags = HideFlags.HideAndDontSave;
            //Dodajê do GameObject komponent Timer (instancjê) i jednoczeœnie j¹ zwracam
            return obj.AddComponent<Timer>();
        }
    }

    private bool isActive = false;
	private float _timeLeft;
	private int _timeLeftInt;
    //To jest wbudowany delegat Action
    //Delegaty s¹ takim wskaŸnikiem do funkcji
    //s¹ one mi potrzebne aby wywo³aæ funkcjê podan¹ w argumencie metody Start
	private Action functionToCall;
    //Takie fikuœne property (gettery i settery)
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
        //Odejmujê wartoœc deltaTime od pozosta³ego czasu
        //DeltaTime to czas który up³yn¹³ miêczy poprzedni¹ a t¹ klatk¹
		TimeLeft -= Time.deltaTime;
        if(TimeLeft < 0)
        {
            StopTimer();
            //Wywo³ujê metodê do wywo³ania przez delegat functionToCall
            functionToCall();
        }
	}
    public void StartTimer()
    {
        isActive = true;
    }
    public void StartTimer(Action functionToCall)
	{
        //This jest wskaŸnikiem, ale ¿e w c# nie ma defakto pojêcia wskaŸnik to i tak urzywamy kropki
        //Zreszt¹ do statycznych rzeczy te¿ kropeczka
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
