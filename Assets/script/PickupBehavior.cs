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
        if (isSpeedBoostActive)
        {
            speedBoostTimer += Time.deltaTime;
            if (speedBoostTimer >= speedBoostDuration)
            {
                playerController.speed = originalSpeed; // Reset to original speed
                isSpeedBoostActive = false;
                Destroy(gameObject); // Destroy the pickup item
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerController != null && !isSpeedBoostActive)
        {
            originalSpeed = playerController.speed; // Store original speed
            playerController.speed *= 2; // Double the speed
            isSpeedBoostActive = true;
            speedBoostTimer = 0f;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }

}