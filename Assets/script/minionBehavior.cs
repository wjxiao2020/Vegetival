using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class minionBehavior : MonoBehaviour
{
    public Transform player;
    public float speed;
    public GameObject explosionVFX;
    public float explosionRadius;
    public float minExplodeDistance;
    public int explodeDamage;
    bool triggered = false;

    // Start is called before the first frame update
    void Awake()
    {
        triggered = false;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        float distance =
            Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z));

                //Debug.Log(onTransformForm);
                FaceTarget(player.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y , player.position.z), step);

        if (distance < minExplodeDistance && !triggered)
        {
            var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);

            explosion.transform.parent = transform;

            Vector3 startPoint = transform.position;
            Vector3 endPoint = transform.position;
            startPoint.y = startPoint.y + explosionRadius;
            endPoint.y = endPoint.y - explosionRadius;
            Collider[] colliders = Physics.OverlapCapsule(startPoint, endPoint, explosionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    var playerHealth = collider.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(explodeDamage);
                    triggered = true;
                }
            }
        }
        
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
