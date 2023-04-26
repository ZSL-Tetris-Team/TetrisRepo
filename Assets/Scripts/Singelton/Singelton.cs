using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance {
        get
        {
            if(_instance == null)
            {
                GameObject obj = new();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }
}
