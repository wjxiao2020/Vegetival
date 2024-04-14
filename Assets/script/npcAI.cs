using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcAI : MonoBehaviour
{
    public enum FSMStates
    {
        Patrol,
        Chase,
        Attack,
    }
    public FSMStates currentState;

    GameObject[] wanderPoints;

    Vector3 nextDestination;

    Animator anim;

    int health;

    int currentDestinationIndex = 0;

    public float enemySpeed = 5;

    public float chaseDistance = 10;

    public GameObject player;

    public GameObject deadVFX;

    public GameObject healthPotionPrefab;
    public GameObject speedPotionPrefab;

    public GameObject hand;

    public GameObject redGlowVFX;
    public GameObject blueGlowVFX;
    private GameObject currentVFX;

    float distanceToPlayer;

    public float attackDistance = 5;

    public float shootRate = 2.0f;

    float elapsedTime = 0;

    Transform deadTransform;

    bool isDead;

    NavMeshAgent agent;

    public Transform enemyEyes;

    public float fieldOfView = 45f;

    private float switchTimer = 5.0f;
    private bool isHealthPotionActive = true;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        hand = GameObject.FindGameObjectWithTag("hand");
        isDead = false;
        agent = GetComponent<NavMeshAgent>();
        currentState = FSMStates.Patrol;
        FindNextPoint();
        SetPotionVFX(isHealthPotionActive);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
        }

        elapsedTime += Time.deltaTime;
        switchTimer -= Time.deltaTime;

        if (switchTimer <= 0)
        {
            isHealthPotionActive = !isHealthPotionActive;
            SetPotionVFX(isHealthPotionActive);
            switchTimer = 5.0f;
        }
    }

    void UpdatePatrolState()
    {
        anim.SetInteger("animState", 1);
        agent.stoppingDistance = 0;
        agent.speed = 3.5f;

        if (Vector3.Distance(transform.position, nextDestination) < 1.5f)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance && IsPlayerInClearFOV())
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 2);
        nextDestination = player.transform.position;
        agent.stoppingDistance = attackDistance;
        agent.speed = 5;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        nextDestination = player.transform.position;
        FaceTarget(nextDestination);
        anim.SetInteger("animState", 3);
        EnemySpellCast();
        Destroy(gameObject, 3);
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemySpellCast()
    {
        if (elapsedTime >= shootRate)
        {
            elapsedTime = 0.0f;
            Invoke("SpellCasting", 2.0f);
        }
    }

    void SpellCasting()
    {
        GameObject potionToSpawn = isHealthPotionActive ? healthPotionPrefab : speedPotionPrefab;
        Vector3 spawnPosition = hand.transform.position + hand.transform.forward * 0.5f; // This will place the spawn position in front of the hand.
        Instantiate(potionToSpawn, spawnPosition, Quaternion.identity);
    }

    void SetPotionVFX(bool isHealth)
    {
        if (currentVFX != null) Destroy(currentVFX);
        GameObject vfxPrefab = isHealth ? redGlowVFX : blueGlowVFX;
        currentVFX = Instantiate(vfxPrefab, hand.transform.position, Quaternion.identity, transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    print("player in sight!");
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
