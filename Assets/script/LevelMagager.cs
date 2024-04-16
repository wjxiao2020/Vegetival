using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMagager : MonoBehaviour
{
    public GameObject gameOverScene;
    public GameObject gameWinScene;

    public GameObject VegeBoss;
    public float createBossInterval = 2f;
    int index = 0;
    public static bool gameEnd;
    public Text levelText;
    float levelTextTimer = 2;
    public GameObject portal;

    public static int bossCount = 0;
    int bossDieCount = 0;

    public Transform spawn;

    public static LevelMagager Instance;
    public int currentLevelNumber;

    // Start is called before the first frame update
    void Start()
    {
        bossCount = 0;
        bossDieCount = 0;

        gameOverScene.SetActive(false);
        gameWinScene.SetActive(false);

        if (spawn == null)
        {
            spawn = GameObject.FindGameObjectWithTag("Spawn").transform;
        }

        if (portal == null)
        {
            portal = GameObject.FindGameObjectWithTag("Portal");
        }

        //VegeBoss[index].gameObject.GetComponent<BossHit>().CreateBoss();
        GameObject.Instantiate(VegeBoss, spawn.position, Quaternion.identity);
        Debug.Log(spawn.position);

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
            print("gameLose");
            gameEnd = true;
            gameOverScene.gameObject.SetActive(true);

            // freeze the game
            //Time.timeScale = 0;

            Invoke("LoadCurrentLevel", 3f);
            //LoadCurrentLevel();
        }
    }

    void LoadCurrentLevel()
    {
        print("LoadCurrentLevel");
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BossDie()
    {
        bossDieCount++;
        if (bossDieCount == bossCount)
        {
            var portalScript = portal.GetComponent<Portal>();
            bossDieCount = 0;
            bossCount = 0;
            portalScript.ReadyToTeleport();

            if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
            {
                gameWin();
            }
        }
        
    }

    void Awake()
    {
        Instance = this;
    }

    public int GetCurrentLevelNumber()
    {
        Debug.Log("Current level number is: " + currentLevelNumber);
        return currentLevelNumber;
    }

    public void SetCurrentLevelNumber(int levelNumber)
    {
        currentLevelNumber = levelNumber;
    }

    /*
    private void CreateNextBoss()
    {
        // initaite next boss
        //VegeBoss[index].gameObject.GetComponent<BossHit>().CreateBoss();
        Instantiate(VegeBoss[index], spawn.position, Quaternion.identity);

        
        var portalScript = portal.GetComponent<Portal>();
        portalScript.ReadyToTeleport();

        index++;
    }
    */
}
