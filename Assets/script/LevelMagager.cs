using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class LevelMagager : MonoBehaviour
{
    public GameObject gameOverScene;
    public GameObject gameWinScene;

    public GameObject[] VegeBoss;
    public float createBossInterval = 2f;
    int index = 0;
    bool gameEnd;

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

        VegeBoss[index].gameObject.GetComponent<BossHit>().CreateBoss();
        index++;

        gameEnd = false;
    }

    public void gameWin()
    {
        if (!gameEnd)
        {
            gameEnd = true;
            gameWinScene.gameObject.SetActive(true);
        }   
    }

    public void gameLose()
    {
        if (!gameEnd)
        {
            gameEnd = true;
            gameOverScene.gameObject.SetActive(true);

            // freeze the game
            Time.timeScale = 0;
        }
    }

    public void BossDie()
    {
        if (index < VegeBoss.Length)
        {
            Invoke("CreateNextBoss", createBossInterval);
            
        }
        else gameWin();
    }

    private void CreateNextBoss()
    {
        // initaite next boss
        VegeBoss[index].gameObject.GetComponent<BossHit>().CreateBoss();

        index++;
    }
}
