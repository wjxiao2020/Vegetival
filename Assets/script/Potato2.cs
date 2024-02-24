using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Potato2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float minDistance = 4;
    public int damageAmount = 20;

    Animator animation;

    [Header("Jumping")]
    public float minYCoordinate = 6.05f;
    public float jumpHeight;
    public float gravity;
    Vector3 moveDirection;

    // abilities
    [Header("First Ability")]
    public float firstAbilityCountDown = 15f;
    private float localFirstAbilityCountDown;

    public float firstAbilityWaitCountDown = 5f;
    private float localFirstAbilityWaitCountDown;

    public float firstAbilityRestTime = 5f;
    private float localFirstAbilityRestTime;

    public float firstAbilityBoostAmount = 3f;
    public float firstAbilityBoostTime = 5f;
    private float localFirstAbilityBoostTime;
    // if the enemy is using the first ability
    bool onFirstAbility = false;
    // enemy stand still before first ability
    bool onFirstAbilityWait = false;
    bool onFirstAbilityRest = false;
    bool onFirstAbilityBoost = false;

    // Start is called before the first frame update
    void Start()
    {
        localFirstAbilityCountDown = firstAbilityCountDown;
        localFirstAbilityWaitCountDown = firstAbilityWaitCountDown;
        localFirstAbilityBoostTime = firstAbilityBoostTime;
        localFirstAbilityRestTime = firstAbilityRestTime;

        animation = gameObject.GetComponent <Animator> ();

        InvokeRepeating
            ("UseFirstAbility", firstAbilityCountDown, firstAbilityCountDown + firstAbilityWaitCountDown + firstAbilityBoostTime + firstAbilityRestTime);

        moveDirection = Vector3.zero;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!onFirstAbility)
        {
            moveEnemy();
        }

        firstAbility();
    }

    // method to move current enemy
    private void moveEnemy()
    {
        float step = moveSpeed * Time.deltaTime;
        float distance =
            Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z));

        if (distance > minDistance)
        {
            // if potato is on ground
            if (transform.position.y <= minYCoordinate)
            {
                // jump
                moveDirection.y = jumpHeight;
            }
            else
            {
                moveDirection.y -= gravity * Time.deltaTime;
                //moveDirection.y = 0.0f;
            }

            transform.LookAt(player);
            transform.position =
                Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y + moveDirection.y, player.position.z), step);
        }
    }

    // invoke this method to enable first ability
    private void UseFirstAbility()
    {
        onFirstAbility = true;
        onFirstAbilityWait = false;
        onFirstAbilityRest = false;
        onFirstAbilityBoost = false;
        localFirstAbilityWaitCountDown = firstAbilityWaitCountDown;
        localFirstAbilityBoostTime = firstAbilityBoostTime;

    }

    private void firstAbility()
    {
        float step = moveSpeed * Time.deltaTime;
        
        if (onFirstAbility)
        {
            if (!onFirstAbilityRest && !onFirstAbilityWait)
            {
                moveEnemy();
            }

            // countdown finishes use first ability
            if (!onFirstAbilityRest && !onFirstAbilityWait && transform.position.y <= minYCoordinate)
            {
                Debug.Log("Potato prepares for first ability");

                // change to serious face
                animation.SetBool("getSerious", true);

                onFirstAbilityWait = true;
                localFirstAbilityCountDown = firstAbilityCountDown;
            }

            if (onFirstAbilityWait)
            {
              
                transform.LookAt(player);
                localFirstAbilityWaitCountDown -= Time.deltaTime;
            }

            if (localFirstAbilityWaitCountDown <= 0)
            {
                Debug.Log("Potato starts first ability");
                // reset the wait time
                localFirstAbilityWaitCountDown = firstAbilityWaitCountDown;
                onFirstAbilityWait = false;
                onFirstAbilityBoost = true;
            }

            // boost the enemy
            if (onFirstAbilityBoost && !onFirstAbilityRest)
            {
 
                float distance =
                Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z));

                if (distance > minDistance)
                {
                    transform.LookAt(player);
                    transform.position =
                        Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), step * firstAbilityBoostAmount);
                }

                localFirstAbilityBoostTime -= Time.deltaTime;

                // finish boosting
                if (localFirstAbilityBoostTime <= 0)
                {
                    Debug.Log("Potato starts rest");

                    // change back to normal face
                    animation.SetBool("getSerious", false);

                    // reset boost time
                    localFirstAbilityBoostTime = firstAbilityBoostTime;
                    onFirstAbilityRest = true;
                    onFirstAbilityBoost = false;
                }
            }

            if (onFirstAbilityRest)
            {
      
                localFirstAbilityRestTime -= Time.deltaTime;
                // rest
                if (localFirstAbilityRestTime <= 0)
                {
                    Debug.Log("Potato's first ability finishes");

                    onFirstAbilityRest = false;
                    onFirstAbility = false;
                    onFirstAbilityBoost = false;

                    localFirstAbilityRestTime = firstAbilityRestTime;
                }
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