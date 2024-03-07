using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    private PlayerController playerController;
    private float originalSpeed;
    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 3f;
    private float speedBoostTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // spin the pickup
        transform.Rotate(new Vector3(0, 0, 0.35f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.AddSpeedBoost();
                gameObject.SetActive(false); // Deactivate the pickup object.
            }
        }
    }

}