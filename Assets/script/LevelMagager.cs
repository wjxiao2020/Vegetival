using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMagager : MonoBehaviour
{
    public GameObject gameOverScene;
    public GameObject gameWinScene;

    public GameObject[] VegeBoss;
    public float createBossInterval = 2f;
    int index = 0;
    bool gameEnd;
    public Text levelText;
    float levelTextTimer = 2;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScene.SetActive(false);
        gameWinScene.SetActive(false);

        index = 0;

        foreach (GameObject obj in VegeBoss)
        {
            //obj.SetActive(false);
        }

        //VegeBoss[index].gameObject.GetComponent<BossHit>().CreateBoss();
        GameObject.Instantiate(VegeBoss[index], new Vector3(52,6,-3), Quaternion.identity);
        index++;

        gameEnd = false;
    }

    void Update()
    {
        SetLevelText();
    }

    void SetLevelText()
    {
        levelTextTimer -= Time.deltaTime;

        if (levelTextTimer <= 0)
        {
            levelText.transform.localScale = Vector3.Lerp(levelText.transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * 2);
        }
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
        //VegeBoss[index].gameObject.GetComponent<BossHit>().CreateBoss();
        GameObject.Instantiate(VegeBoss[index], new Vector3(52, 6, -3), Quaternion.identity);

        index++;
    }
}
