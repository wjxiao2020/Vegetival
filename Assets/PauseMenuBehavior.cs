using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject levelText;
    bool turnOnLevelTextAfterResume;
    GameObject[] crosshairs;
    GameObject currentActiveCrosshair;

    void Start()
    {
        crosshairs = GameObject.FindGameObjectsWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        foreach (GameObject crosshair in crosshairs)
        {
            // remember which crosshair is currently active
            if (crosshair.activeSelf)
            {
                currentActiveCrosshair = crosshair;
            }
            crosshair.SetActive(false);
        }
        if (levelText.activeSelf)
        {
            turnOnLevelTextAfterResume = true;
            levelText.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // set the crosshair that was active before the pause menu appears
        // to be the current active one
        currentActiveCrosshair.SetActive(true);
        if (turnOnLevelTextAfterResume)
        {
            levelText.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
