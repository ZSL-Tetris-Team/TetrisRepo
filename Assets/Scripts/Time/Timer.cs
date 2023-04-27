using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //Moje statyczne property - czyli zarówno gettery i settery w jednym
    //zwraca one za ka¿dym razem now¹ instancje Timer
    //wygl¹da to tak dziwnie bo Timer dziedziczy po MonoBehaviour, czyli jest komponentem, czyli musi byæ przypisany do obiektu GameObject
    //a do czego potrzebujê MoboBahavoiur? Do metody Update która co klatkê bêdzie wykonywa³a odliczanie
    public static Timer NewInstance
    {
        get
        {
            GameObject obj = new();
            obj.name = typeof(Timer).Name;
            //dziêki temu obiekt jest nie widzoczny i nie bêdzie zapisany czy coœ
            obj.hideFlags = HideFlags.HideAndDontSave;
            //dodajê do GameObject komponent Timer (instancjê) i jednoczeœnie j¹ zwracam
            return obj.AddComponent<Timer>();
        }
    }

    private bool isActive = false;
	private float _timeLeft;
	private int _timeLeftInt;
    //to jest wbudowany delegat Action
    //delegaty s¹ takim wskaŸnikiem do funkcji
    //jest on potrzebny aby wywo³aæ funkcjê podan¹ w argumencie metody Start
	private Action functionToCall;
    //taki fikuœne property (gettery i settery)
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
        //doejmujê wartoœc deltaTime od pozosta³ego czasu
        //deltaTime to czas który up³yn¹³ miêczy poprzedni¹ a t¹ klatk¹
		TimeLeft -= Time.deltaTime;
        if(TimeLeft < 0)
        {
            StopTimer();
            //wywo³ujê metodê do wywo³ania przez delegat functionToCall
            functionToCall();
        }
	}
	public void StartTimer(Action functionToCall)
	{
        //this jest wskaŸnikiem, ale ¿e w c# nie ma defakto pojêcia wskaŸnik to i tak urzywamy kropki
        //zreszt¹ do statycznych rzeczy te¿ kropeczka
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
