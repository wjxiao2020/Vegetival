using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliProjectile : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
}
