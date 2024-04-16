using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] buttons;

    private void Update()
    {
        if (!PlayerPrefs.HasKey("LevelNumber"))
        {
            foreach (GameObject button in buttons)
            {
                if (button.CompareTag("Resume"))
                {
                    button.GetComponent<Button>().enabled = false;
                }
            }
        }
        else
        {
            foreach (GameObject button in buttons)
            {
                if (button.CompareTag("Resume"))
                {
                    button.GetComponent<Button>().enabled = true;
                }
            }
        }
    }
    public void StartGame()
    {
        /*
        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            int levelToLoad = PlayerPrefs.GetInt("LevelNumber");
            
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        */

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Resume()
    {
        if (PlayerPrefs.HasKey("LevelNumber"))
        {
            int levelToLoad = PlayerPrefs.GetInt("LevelNumber");

            SceneManager.LoadScene(levelToLoad);
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
