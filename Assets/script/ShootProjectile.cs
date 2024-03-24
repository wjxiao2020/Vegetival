using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 100;
    public Image reticleImage;
    public Image hitReticle;
    public float HitCrossHairActiveTime = 0.3f;
    public Color reticleEnemyColor;
    public Text bulletCountText;
    public int maxBullets = 15;
    private int currentBullets;
    private bool isReloading = false;
    Color originalReticleColor;

    private float lastShootTime = 0f;
    public float fireRate = 0.1f;
    private bool isShooting = false;

    public GameObject weaponPrefab;
    Animator weaponAnimator;
    void Start()
    {
        hitReticle.gameObject.SetActive(false);

        originalReticleColor = reticleImage.color;
        currentBullets = maxBullets;
        UpdateBulletCountUI();

        if (weaponPrefab == null )
        {
            weaponPrefab = GameObject.FindGameObjectWithTag("Weapon");
        }

        weaponAnimator = weaponPrefab.GetComponent<Animator>();
    }

    void Update()
    {
        weaponAnimator.SetBool("fire", false);


        if (Input.GetButtonDown("Fire1") && currentBullets > 0 && !isReloading)
        {
            if (!isShooting)
            {
                StartCoroutine(ShootContinuously());
                isShooting = true;
            }
        }

        // automatically reload if run out of bullets
        if ((Input.GetKeyDown(KeyCode.R) || currentBullets <= 0) && !isReloading)
        {
            if (isShooting)
            {
                StopCoroutine(ShootContinuously());
                isShooting = false; 
            }
            StartCoroutine(Reload());
            weaponAnimator.SetBool("reload", true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(ShootContinuously());
            isShooting = false;
        }
        ReticleEffect();

    }

    IEnumerator ShootContinuously()
    {
        while (Input.GetButton("Fire1") && currentBullets > 0 && !isReloading)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
        isShooting = false;

    }

    void Shoot()
    {
        if (Time.time - lastShootTime >= fireRate)
        {
            lastShootTime = Time.time;

            currentBullets--;
            UpdateBulletCountUI();

            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation) as GameObject;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
            projectile.transform.SetParent(GameObject.Find("ProjectileParent").transform);

            weaponAnimator.SetBool("fire", true);
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        UpdateBulletCountUI();
        yield return new WaitForSeconds(2); 
        currentBullets = maxBullets;
        isReloading = false;
        weaponAnimator.SetBool("reload", false);

        UpdateBulletCountUI();
    }

    void UpdateBulletCountUI()
    {
        if (isReloading)
        {
            bulletCountText.text = "Reloading...";
        }
        else
        {
            bulletCountText.text = "Bullets: " + currentBullets;
        }
    }

    void ReticleEffect()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                ReticleUpdateHitEnemy(reticleImage);
                ReticleUpdateHitEnemy(hitReticle);
            }
            else
            {
                ReticleUpdateNotHitEnemy(reticleImage);
                ReticleUpdateNotHitEnemy(hitReticle);
            }
        }
    }

    private void ReticleUpdateHitEnemy(Image image)
    {
        image.color = Color.Lerp(image.color, reticleEnemyColor, Time.deltaTime * 2);
        image.transform.localScale = Vector3.Lerp(image.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
    }

    private void ReticleUpdateNotHitEnemy(Image image)
    {
        image.color = Color.Lerp(image.color, originalReticleColor, Time.deltaTime * 2);
        image.transform.localScale = Vector3.Lerp(image.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 2);
    }

    public void ChangeCrosshair()
    {
        reticleImage.gameObject.SetActive(false);
        hitReticle.gameObject.SetActive(true);
        StartCoroutine(HitCrossHair());
    }

    IEnumerator HitCrossHair()
    {
        yield return new WaitForSeconds(HitCrossHairActiveTime);

        reticleImage.gameObject.SetActive(true);
        hitReticle.gameObject.SetActive(false);
    }
}
