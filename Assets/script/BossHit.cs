using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHit : MonoBehaviour
{
    public GameObject drop;

    public Slider bossFirstHealthBar;
    public Slider bossSecondHealthBar;
    public GameObject enemyTitle;
    public int BossHealth;
    private int localBossHealth;
    private bool onFirstHealth;

    // initiate common status of enemy
    public void CreateBoss()
    {
        gameObject.SetActive(true);

        localBossHealth = BossHealth;

        bossFirstHealthBar.value = localBossHealth;
        bossSecondHealthBar.value = localBossHealth;

        bossFirstHealthBar.gameObject.SetActive(true);
        bossSecondHealthBar.gameObject.SetActive(true);
        enemyTitle.SetActive(true);

        onFirstHealth = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            // get damage amount of project
            int dmg = FindObjectOfType<VariableHolder>().projectileDamage;

            takeDamage(dmg);
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
        GameObject.FindAnyObjectByType<LevelMagager>().BossDie();

        //Destroy(gameObject, 0.5f);
    }

    // if the boss is dead on the first health
    public bool OnFirstHealth()
    {
        return onFirstHealth;
    }
}
