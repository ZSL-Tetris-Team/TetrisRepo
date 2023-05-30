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
        menu = GameObject.Find("body-menu");
        options = GameObject.Find("body-options");
        scoreboad = GameObject.Find("body-scoreboard");
        instruction = GameObject.Find("body-instruction");
        EventManager.Instance.OnPauseGame.AddListener(EnterMainScreen);
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("Next game");
    }
    public void GoToMenuScene()
    {
        SceneManager.LoadScene("menu");
    }
    public void Return()
    {
        Time.timeScale = 1;
        EventManager.Instance.OnPauseGame.Invoke();
		Time.timeScale = Time.timeScale == 1 ? 0 : 1;
	}
}
