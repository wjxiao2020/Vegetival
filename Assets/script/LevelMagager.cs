using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class LevelMagager : MonoBehaviour
{
    public GameObject gameOverScene;
    public GameObject gameWinScene;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScene.SetActive(false);
        gameWinScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameWin()
    {
        gameWinScene.gameObject.SetActive(true);
    }

    public void gameLose()
    {
        gameOverScene.gameObject.SetActive(true) ;

        // freeze the game
        Time.timeScale = 0;
    }
}
