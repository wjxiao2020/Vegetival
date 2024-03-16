using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Projectie Hit Another Object.");
        // make this projectile invisible once hit into another stuff
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Projectie Hit Another Object.");
        // make this projectile invisible once hit into another stuff
        gameObject.SetActive(false);
    }
}
