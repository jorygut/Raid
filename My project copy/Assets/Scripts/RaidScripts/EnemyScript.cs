using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float health = 3;
    public float freezeCooldown = 5;
    public NavMeshAgent agent;
    public Transform playerPos;
    public LayerMask whatIsGround, whatIsPlayer;
    public Rigidbody enemyRb;
    public GameObject playerObj;
    Fire fireScript;

    //Patrol
    public Vector3 walkpoint;
    bool walkpointSet;
    public float walkpointRange;

    //Attack
    public float timeBetweenAttacks;
    bool attacked;
    public GameObject bullet;

    //States
    public float attackRange;
    bool inAttackRange;
    public bool arrested = false;
    public bool frozen;

    //Animation
    public ParticleSystem blood;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playerPos = GameObject.Find("PlayerSprite").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(agent);
            enemyRb.AddForce(transform.position * 250, ForceMode.Impulse);
            
        }

        while (frozen == true)
        {
            FreezeReset();
        }

        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inAttackRange)
        {
            Patrol();
        }

        if (inAttackRange && frozen == false)
        {
            Attack();
        }
        if (arrested == true)
        {
            enemyRb.constraints = RigidbodyConstraints.FreezePosition;
            attackRange = 0;
            walkpoint = transform.position;
            agent.speed = 0;
        }

        if (fireScript.frozen == true)
        {
            animator.SetBool("isWalking", false);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        blood.Play();
    }
    public void Patrol()
    {
        if (!walkpointSet)
        {
            SearchWalkPoint();
        }
        else if (walkpointSet && !attacked)
        {
            agent.SetDestination(walkpoint);
            animator.SetBool("isWalking", true);
        }

        Vector3 walkpointDistance = transform.position - walkpoint;

        if (walkpointDistance.magnitude < 1f)
        {
            walkpointSet = false;
            animator.SetBool("isWalking", false);
        }
    }
    public void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerPos);
        if (!attacked)
        {
            Shoot();

            attacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }
    public void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkpointSet = true;
        }
    }
    public void Shoot()
    {
        Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 80, ForceMode.Impulse);
        rb.AddForce(transform.up * 3, ForceMode.Impulse);
    }
    private void ResetAttack()
    {
        attacked = false;
    }
    private void FreezeReset()
    {
        freezeCooldown -= 1 * Time.deltaTime;
        if (freezeCooldown <= 0)
        {
            frozen = false;
        }
    }
}
