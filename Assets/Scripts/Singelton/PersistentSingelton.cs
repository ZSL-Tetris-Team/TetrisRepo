using UnityEngine;
using UnityEngine.SceneManagement;

public class PresistentSingelton<T> : MonoBehaviour where T : Component
{
	private static T _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				Scene ActiveScene = SceneManager.GetActiveScene();
				SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
				GameObject obj = new GameObject();
				obj.name = typeof(T).Name;
				obj.hideFlags = HideFlags.DontSave;
				_instance = obj.AddComponent<T>();
				SceneManager.SetActiveScene(ActiveScene);
			}
			return _instance;
		}
	}
	private void OnDestroy()
	{
		if (_instance == null)
		{
			_instance = null;
		}
	}
}

