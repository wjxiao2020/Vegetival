using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] buttons;

    public void StartGame()
    {
        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            int levelToLoad = PlayerPrefs.GetInt("LevelNumber");
            
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HideButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
    }
}
