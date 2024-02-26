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
    private float localShootTime;
    // Start is called before the first frame update
    void Start()
    {
        animation = gameObject.GetComponent<Animator>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        InvokeRepeating("ShootPlayer", firstAbilityStartTime, enemyShootTime + firstAbilityStartTime);

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
            transform.LookAt(player);
            transform.Rotate(new Vector3(0, fixedAngle, 0));
            localShootTime -= Time.deltaTime;
        }

        if (localShootTime <= 0)
        {
            usingGun = false;
            animation.SetBool("onGun", false);
            localShootTime = enemyShootTime;
        }

    }

    private void ShootPlayer()
    {
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
