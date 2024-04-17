using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public string[] scripts;
    public GameObject player;
    public TMP_Text playerText;
    public GameObject lord;
    public TMP_Text lordText;
    // remember when is player's turn
    public int[] playerScriptIndex;
    int currentIndex = 0;
    public static bool finishDialogue = false;


    // Start is called before the first frame update
    void Start()
    {
        finishDialogue = false;
        lord.SetActive(false);
        player.SetActive(false);

        if (playerScriptIndex.Length > 0 && playerScriptIndex[0] == 0 )
        {
            player.SetActive(true);
            playerText.SetText(scripts[currentIndex].ToString());
            Debug.Log(scripts[currentIndex]);
        }
        else
        {
            lord.SetActive(true);
            lordText.SetText(scripts[currentIndex].ToString());
            Debug.Log(scripts[currentIndex]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !PauseMenuBehavior.isGamePaused)
        {
            bool triggered = false;
            currentIndex++;
            if (currentIndex < scripts.Length)
            {
                foreach (int a in playerScriptIndex)
                {
                    // if it is not player's turn
                    if (currentIndex != a)
                    {
                        player.SetActive(false);
                        lord.SetActive(true);
                        lordText.SetText(scripts[currentIndex].ToString());
                        triggered = true;
                    }
                    
                }

                if (!triggered)
                {
                        player.SetActive(true);
                        lord.SetActive(false);
                        var playertext = playerText.GetComponent<TextMeshPro>();
                        playerText.SetText(scripts[currentIndex].ToString());
                }
            }
            else
            {
                player.SetActive(false);
                lord.SetActive(false);
                finishDialogue = true;
            }
        }
    }
}
