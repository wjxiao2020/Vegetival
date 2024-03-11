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

    // trigger the second form of potato
    bool onFirstForm = true;
    public float changeFormSpinDuration = 4f;
    bool onTransformForm = false;

    [Header("Second Ability")]
    public float damageRadius;
    // visual effect for ability
    public GameObject hitEffect;
    public float secondAbilityDamage;

    // Start is called before the first frame update
    void Start()
    {
        localFirstAbilityCountDown = firstAbilityCountDown;
        localFirstAbilityWaitCountDown = firstAbilityWaitCountDown;
        localFirstAbilityBoostTime = firstAbilityBoostTime;
        localFirstAbilityRestTime = firstAbilityRestTime;

        // make sure boss starts with 1st form
        onFirstForm = true;
        onTransformForm = false;
        
        animation = gameObject.GetComponent <Animator> ();

        var firstAbilityRepeatTime =
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
            if (!onFirstAbility)
            {
                moveEnemy();
            }

            firstAbility();
        }

        // trigger 2nd form is enemy is on second health bar
        bool currentOnFirstHealth = gameObject.GetComponent<BossHit>().OnFirstHealth();
        if (!currentOnFirstHealth && onFirstForm)
        {
            onTransformForm = true;
            onFirstForm = false;
            animation.SetTrigger("IntoSecondForm");
            StartCoroutine(SpinEnemy(changeFormSpinDuration));
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
                Debug.Log(onTransformForm);
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
        // (can't delete, to make sure enemy isn't spinning)
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
    }

    private void secondAbility()
    {
        float step = moveSpeed * Time.deltaTime;
        // jump into the sky
        /*
        transform.position =
                            Vector3.MoveTowards(transform.position,
                            new Vector3(player.position.x, 20, player.position.z), step * firstAbilityBoostAmount);
        */
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
        // cancel the previous "invokeRepeat"
        CancelInvoke();
    }
}