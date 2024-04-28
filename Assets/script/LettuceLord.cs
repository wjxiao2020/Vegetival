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
    public int summonBossHealth = 70;
    GameObject currentShield;
    int currentSummonBoss = 0; // the number of summoned boss

    public int fireballAmount = 3;
    int localAmount = 0;
    public Transform hand;

    public GameObject[] spawns;
    public float scaleForHealth = 0.6f;
    public GameObject panel;
    //public static bool immortal;

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
        currentSummonBoss = 0;
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

    // when boss's first health is below 50%, summon the 1st boss
    // when boss's first health is empty, summon the 2nd boss
    private void SummonBoss()
    {
        var health = gameObject.GetComponent<BossHit>();

        if (!onFiring)
        {
            // if first health is below 50% and haven't summon
            if (health.localBossHealth <= health.BossHealth / (1.5) && !summonFirst && !summoning)
            {
                bossIndex = 0;
                animator.SetInteger("animState", 1);
                currentShield = GameObject.Instantiate(shieldPrefab, transform);
                //immortal = true;
                Debug.Log("shield");
                summoning = true;
            }

            if (health.localBossHealth <= 90 && !health.onFirstHealth && !summonSecond && summonFirst && !summoning)
            {
                bossIndex = 1;
                animator.SetInteger("animState", 1);
                currentShield = GameObject.Instantiate(shieldPrefab, transform);
                //immortal = true;
                summoning = true;
            }
        }
    }


    public void SummonBossHelper()
    {
        currentSummonBoss++;
        print("SummonBossHelper");
        GameObject newBoss;
        switch (bossIndex)
        {
            case 0: newBoss = GameObject.Instantiate(potatoPrefab, spawns[0].transform.position, Quaternion.identity);
                newBoss.AddComponent<SummonBehavior>();

                summonFirst = true;
                var script = newBoss.GetComponent<BossHit>();
                script.BossHealth = summonBossHealth;
                break;
            case 1: newBoss = GameObject.Instantiate(broccoliPrefab, spawns[1].transform.position, Quaternion.identity);
                newBoss.AddComponent<SummonBehavior>();

                summonSecond = true;
                var healthScript = newBoss.GetComponent<BossHit>();
                healthScript.BossHealth = summonBossHealth;
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
        summoning = false;
    }

    public void BossDie()
    {
        currentSummonBoss--;
        if (currentSummonBoss <= 0)
        {
           currentShield.gameObject.SetActive(false);
        }
    }
}
