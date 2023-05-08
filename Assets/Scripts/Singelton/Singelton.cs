using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    //statyczne property zwracaj¹ce tylko jedn¹ instancje tej klasy po wiêcej informacji proszê do klasy InputManager
    //myk podobny jak w klasie Timer, ale zwraca tylko jedn¹ instancje
    public static T Instance {
        get
        {
            if(_instance == null)
            {
				GameObject obj = new()
				{
					name = typeof(T).Name,
					hideFlags = HideFlags.HideAndDontSave
				};
				_instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }
}
