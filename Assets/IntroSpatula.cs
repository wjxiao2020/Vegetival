using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IntroSpatula : MonoBehaviour
{
    bool playerInRange = false;
    public GameObject weapon;
    public GameObject portal;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        weapon.SetActive(false);
    }

    private void Update()
    {
        if (!DialogueManager.finishDialogue)
        {
            text.SetActive(false);
        }
        else
        {
            text.SetActive(true); 
        }

        if (Input.GetKeyDown(KeyCode.E) && DialogueManager.finishDialogue)
        {
            if (playerInRange)
            {
                AudioSource lootSFX = GetComponent<AudioSource>();
                lootSFX.time = 2;
                lootSFX.Play();
                
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                //gameObject.SetActive(false);
                //var weapon = GameObject.FindGameObjectWithTag("Weapon");
                weapon.SetActive(true);
                var levelmanager = GameObject.FindAnyObjectByType<LevelMagager>();
                levelmanager.tutorial = false;

                var portalScript = portal.GetComponent<Portal>();
                portalScript.ReadyToTeleport();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("hihi");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


}
