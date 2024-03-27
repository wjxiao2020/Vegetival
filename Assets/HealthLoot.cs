using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoot : MonoBehaviour
{

    public int healthAmount = 20;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeHealth(healthAmount);
            Destroy(gameObject, 0.5f);
        }
    }
}

