using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singelton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance {
        get
        {
            if(_instance == null)
            {
                GameObject obj = new()
                {
                    name = typeof(T).Name,
                    hideFlags = HideFlags.DontSave
				};
				_instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }
	private void OnEnable()
	{
        SceneManager.sceneUnloaded += ResetInstance;
	}
	private void ResetInstance(Scene scene)
    {
        _instance = null;
        SceneManager.sceneUnloaded -= ResetInstance;
    }
}
