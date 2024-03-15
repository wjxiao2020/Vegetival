using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;


public class Potato2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float minDistance = 4;
    public int damageAmount = 20;
    public float changeFormSpinDuration = 4f;



    Animator animation;
    public GameObject shield;
    GameObject currentShield;

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

    float firstAbilityRepeatTime;

    // trigger the second form of potato
    bool onFirstForm = true;
  
    bool onTransformForm = false;

    [Header("Second Ability")]
    public float secondAbilityCountDown = 15f;
    //private float localFirstAbilityCountDown;
    //public float secondAbilityWaitCountDown = 5f;

    public float damageRadius;
    public float secondAbilityJumpHeight;
    public float secondAbilityFallSpeed;
    public int onSkyTime = 2;
    // visual effect for ability
    public GameObject hitEffect;
    public int secondAbilityDamage;
    public float jumpSpeed = 3f;


    Vector3 directionToLift;
    Vector3 targetPosition;
    Vector3 playerRecordPosition;
    bool onAbilityLift;
    bool onAbilityDown;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        localFirstAbilityCountDown = firstAbilityCountDown;
        localFirstAbilityWaitCountDown = firstAbilityWaitCountDown;
        localFirstAbilityBoostTime = firstAbilityBoostTime;
        localFirstAbilityRestTime = firstAbilityRestTime;

        // make sure boss starts with 1st form
        onFirstForm = true;
        onTransformForm = false;

        onAbilityLift = false;
        onAbilityDown = false;

        animation = gameObject.GetComponent<Animator>();

        firstAbilityRepeatTime =
            firstAbilityCountDown + firstAbilityWaitCountDown + firstAbilityBoostTime + firstAbilityRestTime;

        InvokeRepeating
            ("UseFirstAbility", firstAbilityCountDown, firstAbilityRepeatTime);

        moveDirection = Vector3.zero;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // if spinning, don't do anything except spinning
        if (!onTransformForm)
        {
            if (!onFirstAbility && !onAbilityDown)
            {
                moveEnemy();
            }

            firstAbility();
            SecondAbility();
        }

        // trigger 2nd form is enemy is on second health bar
        bool currentOnFirstHealth = gameObject.GetComponent<BossHit>().OnFirstHealth();
        if (!currentOnFirstHealth && onFirstForm)
        {
            // back up and enable shield
            currentShield = GameObject.Instantiate(shield, transform);
            currentShield.transform.parent = null;

            onTransformForm = true;
            onFirstForm = false;
            animation.SetTrigger("IntoSecondForm");
            StartCoroutine(SpinEnemy(changeFormSpinDuration));

            // cancel the previous "invokeRepeat"
            CancelInvoke();

            // assume second ability last for 6f
            float secondAbilityDuration = 12.5f;

            firstAbilityRepeatTime =
            5f + firstAbilityWaitCountDown + 
            firstAbilityBoostTime + firstAbilityRestTime  + secondAbilityDuration;

            float secondAbilityRepeatTime =
                5f + firstAbilityRepeatTime;

            // first ability get invoked after second ability
            InvokeRepeating
            (nameof(UseFirstAbility), 4f + secondAbilityDuration, firstAbilityRepeatTime);

           secondAbilityCountDown = firstAbilityRepeatTime + secondAbilityCountDown;

            InvokeRepeating
           (nameof(UseSecondAbility), 5f, secondAbilityRepeatTime);
        }
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

            if (!onTransformForm)
            {
                //Debug.Log(onTransformForm);
                FaceTarget(player.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y + moveDirection.y, player.position.z), step);
            }
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

    // implementation of first Ability
    private void firstAbility()
    {
        if (!onTransformForm)
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
                    FaceTarget(player.position);
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
                BoostEnemy(step);

                // rest enemy and countdown rest time
                RestCountDown();
            }
        }
    }

    // part of enemy's first ability
    private void BoostEnemy(float step)
    {
        if (onFirstAbilityBoost && !onFirstAbilityRest)
        {
            float distance =
            Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z));

            if (distance > minDistance)
            {
                FaceTarget(player.position);

                transform.position =
                    Vector3.MoveTowards(transform.position,
                    new Vector3(player.position.x, transform.position.y, player.position.z), step * firstAbilityBoostAmount);
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
    }

    // part of enemy's first ability
    private void RestCountDown()
    {
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

    private void SecondAbility()
    {
        if (onAbilityLift)
        {
            float step = jumpSpeed * Time.deltaTime;
            //controller.Move(directionToLift * Time.deltaTime);
            transform.position =
            Vector3.MoveTowards(transform.position, 
            targetPosition, step);

        }
        
        if (transform.position.y >= secondAbilityJumpHeight)
        {
            // float for some time
            StartCoroutine(CountdownFloating(onSkyTime));
        }

        if (onAbilityDown)
        {
            // disable shield when falling
            Destroy(currentShield);

            float step = secondAbilityFallSpeed * Time.deltaTime;
            //transform.po.y -= gravity * secondAbilityFallSpeed * Time.deltaTime;
            float distance =
               Vector3.Distance(transform.position, playerRecordPosition);

            if (distance > minDistance)
            {

                transform.position =
            Vector3.MoveTowards(transform.position,
            playerRecordPosition, step);
            }
            // enemy hit ground
            else
            {
                onAbilityDown = false;
                Instantiate(hitEffect, new Vector3(transform.position.x, 2f, transform.position.z), Quaternion.identity);

                Vector3 startPoint = transform.position;
                Vector3 endPoint = transform.position;
                startPoint.y = startPoint.y + damageRadius;
                endPoint.y = endPoint.y - damageRadius;

                Collider[] colliders = Physics.OverlapCapsule(startPoint, endPoint, damageRadius);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        var playerHealth = collider.GetComponent<PlayerHealth>();
                        playerHealth.TakeDamage(secondAbilityDamage);
                    }
                }
            }
            
        }

    }

    private void UseSecondAbility()
    {
        currentShield = GameObject.Instantiate(shield, transform);

        onAbilityLift = true;
        onAbilityDown = false;

        //targetPosition = transform.forward;
        targetPosition = transform.position;
        targetPosition.y = secondAbilityJumpHeight;
    }

    private IEnumerator CountdownFloating(int timer)
    {
        FaceTarget(player.position);
        while (timer > 0)
        {
            // for each timer, sleep for 1 second
            yield return new WaitForSeconds(1);
            
            timer--;
        }
        onAbilityLift = false;
        onAbilityDown = true;

        playerRecordPosition = new Vector3(player.position.x, player.position.y + 3, player.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 10 * Time.deltaTime);
    }

    private IEnumerator SpinEnemy(float duration)
    {
        float time = 0;
        
        while (time < duration)
        {
          transform.Rotate(new Vector3(0, 30 ,0));

            time += Time.deltaTime;
            yield return null;
        }
        onFirstForm = false;
        onTransformForm = false;

        // disable shield
        Destroy(currentShield);
    }

    // display second ability radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);

    }
}