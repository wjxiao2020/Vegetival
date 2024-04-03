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

    // Start is called before the first frame update
    void Awake()
    {
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

        var panel = GameObject.FindGameObjectWithTag("BossPanel");

        foreach (var healthbar in healthbars)
        {
            healthbar.transform.parent = panel.transform;
        }
        var currentScale = panel.transform.localScale;
        panel.transform.localScale = 
            new Vector3(currentScale.x * scaleForHealth, currentScale.y * scaleForHealth, currentScale.z * scaleForHealth);

    }
}
