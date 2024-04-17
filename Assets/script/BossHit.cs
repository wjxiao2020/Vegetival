using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHit : MonoBehaviour
{
    public GameObject drop;
    public float cheerVolume;
    public AudioClip hitSFX;

    public Slider bossFirstHealthBar;
    public Slider bossSecondHealthBar;
    public GameObject enemyTitle;
    public int BossHealth;
    public int localBossHealth;
    public bool onFirstHealth;
    private int hitAmout;

    // initiate common status of enemy
    private void Awake()
    {
        enemyTitle.SetActive(true);

        //gameObject.SetActive(true);

        localBossHealth = BossHealth;

        bossFirstHealthBar.value = localBossHealth;
        bossSecondHealthBar.value = localBossHealth;

        var canvas = GetComponentInChildren<Canvas>();
        canvas.transform.parent = null;

        //bossFirstHealthBar.gameObject.SetActive(true);
        //bossSecondHealthBar.gameObject.SetActive(true);
        //enemyTitle.SetActive(true);

        onFirstHealth = true;
        hitAmout = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            if ((gameObject.CompareTag("Lettuce") && !LettuceLord.immortal) || !gameObject.CompareTag("Lettuce"))
            {
                // get damage amount of project
                int dmg = FindObjectOfType<VariableHolder>().projectileDamage;

                hitAmout++;
                if (hitAmout % 10 == 1)
                {
                    AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, 0.5f);
                }

                takeDamage(dmg);
            }

        }
    }

    private void takeDamage(int damage)
    {
        localBossHealth -= damage;
        if (onFirstHealth)
        {
            bossFirstHealthBar.value = localBossHealth;
        }
        else
        {
            bossSecondHealthBar.value = localBossHealth;
        }

        if (localBossHealth <= 0 && onFirstHealth)
        {
            onFirstHealth = false;
            localBossHealth = BossHealth;
        }

        if (!onFirstHealth && localBossHealth <=0) 
        {
            PlayCheer.Play(cheerVolume);
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        // hide all enemy information
        bossFirstHealthBar.gameObject.SetActive(false);
        bossSecondHealthBar.gameObject.SetActive(false);

        enemyTitle.SetActive(false);

        Vector3 spawnPosition = new Vector3(transform.position.x, 4f, transform.position.z);

        GameObject droppedItem = Instantiate(drop, spawnPosition, drop.transform.rotation);

        gameObject.SetActive(false);

        // reset value
        bossSecondHealthBar.value = BossHealth;
        bossFirstHealthBar.value = BossHealth;

        // call next boss
        FindAnyObjectByType<LevelMagager>().BossDie();

        Destroy(gameObject, 0.5f);
    }

    // if the boss is dead on the first health
    public bool OnFirstHealth()
    {
        return onFirstHealth;
    }
}
