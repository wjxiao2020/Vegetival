using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLoot : MonoBehaviour
{

    public int healthAmount = 20;

    public GameObject player;
    public GameObject bottle;
    
    public Text interactText;

    private bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        interactText.gameObject.SetActive(false);
        
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            AudioSource lootSFX = GetComponent<AudioSource>();
            lootSFX.time = 2;
            lootSFX.Play();
            print("lootSFX played");
            var playerHealth = player.GetComponent<PlayerHealth>();
            if(bottle.activeSelf)
            {
                playerHealth.TakeHealth(healthAmount);
            }
            bottle.SetActive(false);
            //gameObject.SetActive(false);
            

            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(true);
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
            isInRange = false;
        }
    }
}


