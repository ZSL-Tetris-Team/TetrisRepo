using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
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
    }
    // Update is called once per frame
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("Next game");
    }
    public void GoToMenuScene()
    {
        SceneManager.LoadScene("menu");
    }
}
