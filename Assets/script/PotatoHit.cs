using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotatoHit : MonoBehaviour
{
    public GameObject drop;

    public Slider potatoHealthBar_first;
    public Slider potatoHealthBar_second;
    public int potatoHealth;
    private int localPotatoHealth;
    private bool onFirstHealth;

    // Start is called before the first frame update
    void Start()
    {
        localPotatoHealth = potatoHealth;
        potatoHealthBar_first.value = localPotatoHealth;
        potatoHealthBar_second.value = localPotatoHealth;

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
        localPotatoHealth -= damage;
        if (onFirstHealth)
        {
            potatoHealthBar_first.value = localPotatoHealth;
        }
        else
        {
            potatoHealthBar_second.value = localPotatoHealth;
        }

        if (localPotatoHealth <= 0 && onFirstHealth)
        {
            onFirstHealth = false;
            localPotatoHealth = potatoHealth;
        }

        if (!onFirstHealth && localPotatoHealth <=0) 
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, 4f, transform.position.z);

        GameObject droppedItem = Instantiate(drop, spawnPosition, drop.transform.rotation);

        gameObject.SetActive(false);

        // call game over
        GameObject.FindAnyObjectByType<LevelMagager>().gameWin();

        Destroy(gameObject, 0.5f);
    }
}
