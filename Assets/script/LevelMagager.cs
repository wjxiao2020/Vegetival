using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class LevelMagager : MonoBehaviour
{
    public GameObject gameOverScene;
    public GameObject gameWinScene;

    public GameObject[] VegeBoss;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScene.SetActive(false);
        gameWinScene.SetActive(false);

        index = 0;

        foreach (GameObject obj in VegeBoss)
        {
            obj.SetActive(false);
        }

        VegeBoss[index].gameObject.SetActive(true);
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

    public void BossDie()
    {
        if (index < VegeBoss.Length)
        {
            // initaite next boss
            VegeBoss[index].gameObject.SetActive(true);
            index++;
        }
        else gameWin();
    }
}
