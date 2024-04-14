using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupBehavior : MonoBehaviour
{
    public AudioClip pickUpClip;

    private PlayerController playerController;
    private float originalSpeed;
    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 3f;
    private float speedBoostTimer = 0f;
    private bool isInRange = false;
    public Text interactText;

    // Start is called before the first frame update
    void Start()
    {
        interactText.gameObject.SetActive(false);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            interactText.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(pickUpClip, Camera.main.transform.position, 1);
           
            if (playerController != null)
            {
                playerController.AddSpeedBoost();
            }
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
