using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuManager : MonoBehaviour
{
    GameObject menu;
    GameObject options;
    GameObject scoreboad;
    GameObject instruction;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Loaded");
        menu = GameObject.Find("body-menu");
        options = GameObject.Find("body-options");
        scoreboad = GameObject.Find("body-scoreboard");
        instruction = GameObject.Find("body-instruction");
        EventManager.Instance.OnPauseGame.AddListener(EnterMainScreen);
		EventManager.Instance.OnPauseGame.AddListener(ToggleCanvas);
		EnterMainScreen();
		gameObject.SetActive(false);
    }
    public void ToggleCanvas()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    public void EnterMainScreen()
    {
        menu.SetActive(true);
        options.SetActive(false);
        scoreboad.SetActive(false);
        instruction.SetActive(false);
    }
    public void EnterOptions() {
        menu.SetActive(false);
        options.SetActive(true);
        scoreboad.SetActive(false);
        instruction.SetActive(false);
    }
    public void EnterMenu() {
        menu.SetActive(true);
        options.SetActive(false);
        scoreboad.SetActive(false);
        instruction.SetActive(false);
    }
    public void EnterScoreboard() {
        menu.SetActive(false);
        scoreboad.SetActive(true);
        instruction.SetActive(false);
        options.SetActive(false);
    }
    public void EnterInstruction()
    {
        menu.SetActive(false);
        instruction.SetActive(true);
        options.SetActive(false);
        scoreboad.SetActive(false);
    }
    public void StartPlaying()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ReStartPlaying()
    {
        Time.timeScale = 1;
        if (GameManager.Instance.State != GameManager.States.Lost)
            EventManager.Instance.OnLost.Invoke();
        gameObject.SetActive(true);
        SceneManager.LoadScene("MainScene");
    }
    public void GoToMenuScene()
    {
        Time.timeScale = 1;
		if (GameManager.Instance.State != GameManager.States.Lost)
			EventManager.Instance.OnLost.Invoke();
		SceneManager.LoadScene("menu");
    }
    public void Resume()
    {
        Time.timeScale = 1;
        EventManager.Instance.OnPauseGame.Invoke();
	}
}
