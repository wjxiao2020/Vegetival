using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject drop;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Instantiate(drop, transform.position, transform.rotation);

        gameObject.SetActive(false);

        Destroy(gameObject, 0.5f);
    }

}
