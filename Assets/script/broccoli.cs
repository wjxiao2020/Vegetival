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

    private bool usingGun = false;

    // fix broccoli's y coordinate
    private float startYCo;

    Animator animation;

    public float firstAbilityStartTime;
    public float enemyShootTime;
    public GameObject projectilePrefab;
    GameObject GunPrefab;
    public float projectileSpeed;
    public float shotInterval;
    float localShotInterval;
    private float localShootTime;

    // Start is called before the first frame update
    void Start()
    {
        localShotInterval = shotInterval;
        animation = gameObject.GetComponent<Animator>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        InvokeRepeating("TriggerShootPlayer", firstAbilityStartTime, enemyShootTime + firstAbilityStartTime);

        GunPrefab = GameObject.FindGameObjectWithTag("BroccoliWeapon");
        // hide gun
        GunPrefab.gameObject.SetActive(false);
        startYCo = transform.position.y;
        localShootTime = enemyShootTime;
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
            GunPrefab.gameObject.SetActive(false);
            animation.SetBool("onGun", false);
            localShootTime = enemyShootTime;
        }

    }

    private void Shot()
    {
        localShotInterval -= Time.deltaTime;

        if (localShotInterval <= 0)
        {
            GameObject projectile =
                 Instantiate(projectilePrefab, GunPrefab.transform.position, transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();


            transform.Rotate(new Vector3(0, -fixedAngle, 0));
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

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
