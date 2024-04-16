using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSpatula : MonoBehaviour
{
    bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerInRange)
            {
                gameObject.SetActive(false);
                var weapon = GameObject.FindGameObjectWithTag("Weapon");
                weapon.SetActive(true);
                var levelmanager = GameObject.FindAnyObjectByType<LevelMagager>();
                levelmanager.tutorial = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
