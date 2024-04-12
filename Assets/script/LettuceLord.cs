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

    public GameObject[] spawns;
    public float scaleForHealth = 0.6f;
    public GameObject panel;

    Animator animator;
    // if the boss is summoning another boss
    bool summoning = false;
    bool onFiring = false;

    // Start is called before the first frame update
    void Awake()
    {
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

        Invoke("SummonBoss", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(player.transform.position);

        CastFireBall();
    }

    void CastFireBall()
    {
        if (!onFiring)
        {
            animator.SetInteger("animState", 2);
            onFiring=true;
            StartCoroutine(FirstPause());
        }
    }

    IEnumerator FirstPause()
    {
        yield return new WaitForSeconds(3);
        animator.SetInteger("animState", 0);
        onFiring = false;
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 10 * Time.deltaTime);
    }

    private void SummonBoss()
    {
        var potato = GameObject.Instantiate(potatoPrefab, spawns[0].transform.position, Quaternion.identity);
        var broccoli = GameObject.Instantiate(broccoliPrefab, spawns[1].transform.position, Quaternion.identity);

        GameObject[] healthbars = GameObject.FindGameObjectsWithTag("BossCanvas");

        foreach (var healthbar in healthbars)
        {
            healthbar.transform.parent = panel.transform;
            healthbar.transform.localScale *= 0.7f;

        }
        
        var currentScale = panel.transform.localScale;
        
        panel.transform.localScale = 
            new Vector3(currentScale.x * scaleForHealth, currentScale.y * scaleForHealth, currentScale.z * scaleForHealth);
        
        
    }
}
