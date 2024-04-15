using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupIntro : MonoBehaviour
{
    public GameObject bluePickupPage;
    public GameObject redPickupPage;

    // there's bug when set button as parent of panel
    public GameObject blueButton;
    public GameObject redButton;

    public static bool onPickupMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        bluePickupPage.SetActive(true);
        redButton.SetActive(false);

        blueButton.SetActive(true);
        redPickupPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextPage()
    {
        bluePickupPage.SetActive(false);
        redPickupPage.SetActive(true);

        blueButton.SetActive(false);
        redButton.SetActive(true);
    }

    public void ResumeGame()
    {
        onPickupMenu = false;
        PauseMenuBehavior.isGamePaused = false;

        gameObject.SetActive(false);

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void InvokeIntro()
    {
        onPickupMenu = true;
        PauseMenuBehavior.isGamePaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
