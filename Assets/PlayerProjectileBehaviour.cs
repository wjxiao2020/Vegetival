using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileBehaviour : MonoBehaviour
{
    ShootProjectile script;

    void Start()
    {
        script = GameObject.FindAnyObjectByType<ShootProjectile>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Lettuce"))
        {
            script.ChangeCrosshair();
        }
    }
}
