using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    int currentHealth;
    public Slider healthSlider;
    public float cheerVolume;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (!PlayCheer.isPlaying() || currentHealth <= 0) {
            PlayCheer.Play(cheerVolume);
        }
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            PlayerDies();
        }

        Debug.Log("Current Health: " + currentHealth);
    }

    void PlayerDies()
    {
        // player dies gameover
        GameObject.FindAnyObjectByType<LevelMagager>().gameLose();

        Debug.Log("Player is dead ...");
        transform.Rotate(-90, 0, 0, Space.Self);
    }
}
