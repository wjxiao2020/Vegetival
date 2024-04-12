using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LettuceLord : MonoBehaviour
{
    public GameObject player;
    public GameObject potatoPrefab;
    public GameObject broccoliPrefab;
    public GameObject fireballVFX;
    public GameObject shieldPrefab;
    GameObject currentShield;

    public int fireballAmount = 3;
    int localAmount = 0;
    public Transform hand;

    public GameObject[] spawns;
    public float scaleForHealth = 0.6f;
    public GameObject panel;

    Animator animator;
    // if the boss is summoning another boss
    bool summoning = false;
    bool onFiring = false;
    bool summonFirst = false;
    bool summonSecond = false;

    int bossIndex = 0;


    // Start is called before the first frame update
    void Awake()
    {
        summonFirst = false;
        summonSecond = false;
        localAmount = 0;
        onFiring = false;
        animator = GetComponentInChildren<Animator>();
        summoning = false;
        LevelMagager.bossCount++;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (spawns.Length == 0)
        {
            spawns = GameObject.FindGameObjectsWithTag("Level3Spawn");
        }

        //Invoke("SummonBoss", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(player.transform.position);

        SummonBoss();
        CastFireBall();
    }

    void CastFireBall()
    {
        if (!onFiring && !summoning)
        {
            if (localAmount <= fireballAmount)
            {
                localAmount++;
                animator.SetInteger("animState", 2);
                onFiring = true;
                StartCoroutine(FirstPause());
            }
            // if boss already fired given amount of fire ball
            else
            {
                StartCoroutine(FireCooldown());
            }
        }
     
    }

    // invoked in the animataion 
    public void FireFireball()
    {
        Instantiate(fireballVFX, hand.transform.position,
                hand.transform.rotation);
    }

    IEnumerator FirstPause()
    {
        yield return new WaitForSeconds(3);
        animator.SetInteger("animState", 0);
        onFiring = false;
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(3);
        animator.SetInteger("animState", 0);
        localAmount = 0;
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 10 * Time.deltaTime);
    }

    // when boss's health is below 50%, summon another boss
    // when boss's first health is empty, summon another boss
    private void SummonBoss()
    {
        var health = gameObject.GetComponent<BossHit>();

        if (!onFiring)
        {
            // if health is below 50% and haven't summon
            if (health.localBossHealth <= health.BossHealth / 2 && !summonFirst && !summoning)
            {
                bossIndex = 0;
                animator.SetInteger("animState", 1);
                currentShield = GameObject.Instantiate(shieldPrefab, transform);
                summoning = true;
            }

            if (health.localBossHealth <= 10 && !summonSecond && !summoning)
            {
                bossIndex = 1;
                animator.SetInteger("animState", 1);
                currentShield = GameObject.Instantiate(shieldPrefab, transform);
                summoning = true;
            }
        }
     
    }


    public void SummonBossHelper()
    {
        GameObject newBoss;
        switch (bossIndex)
        {
            case 0: newBoss = GameObject.Instantiate(potatoPrefab, spawns[0].transform.position, Quaternion.identity);
                summonFirst = true;
                break;
            case 1: newBoss = GameObject.Instantiate(broccoliPrefab, spawns[1].transform.position, Quaternion.identity);
                summonSecond = true;
                break;
        }

        if (!summonSecond)
        {
            var currentScale = panel.transform.localScale;

            panel.transform.localScale =
                new Vector3(currentScale.x * scaleForHealth, currentScale.y * scaleForHealth, currentScale.z * scaleForHealth);
        }

        GameObject[] healthbars = GameObject.FindGameObjectsWithTag("BossCanvas");

        foreach (var healthbar in healthbars)
        {
            healthbar.transform.parent = panel.transform;
            healthbar.transform.localScale *= 0.7f;
            healthbar.transform.gameObject.tag = "Untagged";
        }

        // wait for the animation to complete
        StartCoroutine(SummonRest());

    }

    IEnumerator SummonRest()
    {
        yield return new WaitForSeconds(2);
        animator.SetInteger("animState", 0);
        currentShield.gameObject.SetActive(false);
        summoning = false;
    }
}
