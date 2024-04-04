using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class broccoli : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float minDistance = 2;
    public int damageAmount = 20;
    public float fixedAngle;
    public float minYCoordinate;
    public float gravity;
    private bool usingGun = false;

    // fix broccoli's y coordinate
    private float startYCo;

    Animator animation;

    public float firstAbilityStartTime;
    public float enemyShootTime;
    public GameObject projectilePrefab;
    GameObject GunPrefab;
    public GameObject gunMuzzle;
    public float projectileSpeed;
    public float shotInterval;
    float localShotInterval;
    private float localShootTime;

    public AudioClip shootSFX;
    public float SFXVolume = 0.1f;

    public AudioClip showUpSFX;

    [Header("Summon Minion")]
    public GameObject minionPrefab;
    public float minionSpawnRadius = 3f;
    public int minionNumber = 3;

    private void Awake()
    {
        LevelMagager.bossCount++;

        localShotInterval = shotInterval;
        animation = gameObject.GetComponent<Animator>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        InvokeRepeating("TriggerShootPlayer", firstAbilityStartTime, enemyShootTime + firstAbilityStartTime);

        GunPrefab = GameObject.FindGameObjectWithTag("BroccoliWeapon");
        // hide gun
        //GunPrefab.gameObject.SetActive(false);
        startYCo = transform.position.y;
        localShootTime = enemyShootTime;

        AudioSource.PlayClipAtPoint(showUpSFX, Camera.main.transform.position, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, player.position);

        if (!usingGun && distance > minDistance)
        {
            transform.LookAt(player);
            transform.Rotate(new Vector3(0, fixedAngle, 0));
            transform.position = 
                Vector3.MoveTowards(transform.position, new Vector3(player.position.x, startYCo, player.position.z), step);
        }

        if (usingGun)
        {
            Shot();
            transform.LookAt(player);
            transform.Rotate(new Vector3(0, fixedAngle, 0));
            localShootTime -= Time.deltaTime;
        }

        if (localShootTime <= 0)
        {
            usingGun = false;
            //GunPrefab.gameObject.SetActive(false);
            animation.SetBool("onGun", false);
            localShootTime = enemyShootTime;
        }

        if (transform.position.y > minYCoordinate)
        {
            transform.position = 
                new Vector3(transform.position.x, transform.position.y - gravity*Time.deltaTime, transform.position.z);
        }

    }

    private void Shot()
    {
        localShotInterval -= Time.deltaTime;

        if (localShotInterval <= 0)
        {
            GameObject projectile =
                 Instantiate(projectilePrefab, gunMuzzle.transform.position, transform.rotation) as GameObject;

            AudioSource.PlayClipAtPoint(shootSFX, transform.position, SFXVolume);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // because there's rotation applied on gun's prefab
            // aim the player
            rb.transform.LookAt(player.position);

            rb.AddForce(rb.transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("BroccoliProjectileParent").transform);

            localShotInterval = shotInterval;
        }

    }

    // invoked repeatedly in start()
    private void TriggerShootPlayer()
    {
        GunPrefab.gameObject.SetActive(true);
        usingGun = true;
        animation.SetBool("onGun", true);

        SummonMinion();
    }

    private void SummonMinion()
    {
        int i = 0;
        while (i < minionNumber)
        {
            float xCo = 
                Random.Range(transform.position.x + minionSpawnRadius, transform.position.x - minionSpawnRadius);
            float zCo =
                Random.Range(transform.position.z + minionSpawnRadius, transform.position.z - minionSpawnRadius);
            Instantiate(minionPrefab, new Vector3(xCo, transform.position.y, zCo), Quaternion.identity);
            i++;
        }
    }

    // display minion spawn radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minionSpawnRadius);

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
