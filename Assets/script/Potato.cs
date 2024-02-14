using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float minDistance = 2;
    public int damageAmount = 20;

    // abilities
    [Header("First Ability")]
    public float firstAbilityCountDown = 15f;
    private float localFirstAbilityCountDown;

    public float firstAbilityWaitCountDown = 5f;
    private float localFirstAbilityWaitCountDown;

    public float firstAbilityBoost = 3f;
    public float firstAbilityBoostTime = 5f;
    private float localFirstAbilityBoostTime;
    // if the enemy is using the first ability
    bool onFirstAbility = false;
    // enemy stand still before first ability
    bool onFirstAbilityWait = false;

    // Start is called before the first frame update
    void Start()
    {
        localFirstAbilityCountDown = firstAbilityCountDown;
        localFirstAbilityWaitCountDown = firstAbilityWaitCountDown;
        localFirstAbilityBoostTime = firstAbilityBoostTime;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        float distance = 
            Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z));

        if (!onFirstAbility && distance > minDistance)
        {
            transform.LookAt(player);
            transform.position = 
                Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), step);
        }

        firstAbility(step);
    }

    private void firstAbility(float step)
    {
        // countdown for first ability
        if (!onFirstAbility)
        {
            localFirstAbilityCountDown -= Time.deltaTime;
        }

        // countdown finishes use first ability
        if (localFirstAbilityCountDown <= 0)
        {
            Debug.Log("Potato prepares for first ability");

            onFirstAbilityWait = true;
            onFirstAbility = true;
            localFirstAbilityCountDown = firstAbilityCountDown;
        }

        if (onFirstAbilityWait)
        {
            localFirstAbilityWaitCountDown -= Time.deltaTime;
        }

        if (localFirstAbilityWaitCountDown <= 0)
        {
            Debug.Log("Potato starts first ability");
            // reset the wait time
            localFirstAbilityWaitCountDown = firstAbilityWaitCountDown;
            onFirstAbilityWait = false;
        }

        // boost the enemy
        if (!onFirstAbilityWait && onFirstAbility)
        {
            transform.LookAt(player);
            transform.position = 
                Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), step * firstAbilityBoost);

            localFirstAbilityBoostTime -= Time.deltaTime;

            // finish boosting
            if (localFirstAbilityBoostTime <= 0)
            {
                Debug.Log("Potato's first ability finishes");
                // reset boost time
                localFirstAbilityBoostTime = firstAbilityBoostTime;
                onFirstAbility = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }
}