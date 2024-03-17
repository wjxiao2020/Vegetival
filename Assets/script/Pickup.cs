using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Text pickupCounterText; 
    private int pickupCount = 0; 
    private GameObject currentPickupObject = null;
    public Text interactText;
    

    void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentPickupObject != null)
        {
            pickupCount++; 
            UpdatePickupCounterUI(); 
            Destroy(currentPickupObject);
            currentPickupObject = null;
            interactText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            currentPickupObject = other.gameObject;
            interactText.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            
            if (currentPickupObject == other.gameObject)
            {
                currentPickupObject = null;
            }
            interactText.gameObject.SetActive(false);
        }
        
    }

    private void UpdatePickupCounterUI()
    {
        if (pickupCounterText != null)
        {
            pickupCounterText.text = "Pickup: " + pickupCount.ToString();
        }
    }
}
