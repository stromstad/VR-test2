using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent agent;

    public GameObject model;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Main Camera").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = model.GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        //if (!walkPointSet) SearchWalkPoint();

        //if (walkPointSet)
        //    agent.SetDestination(walkPoint);

        //Vector3 distanceToWalkPoint = transform.position - walkPoint;

        ////Walkpoint reached
        //if (distanceToWalkPoint.magnitude < 1f)
        //{
        //    walkPointSet = false;
        //}
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit by something {collision.gameObject.name}");
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("Hit by bullet");
            TakeDamage(Random.Range(30, 50));
        }
    }
    private void ChasePlayer()
    {
        animator.SetFloat("MoveSpeed", 2.0f);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (!alreadyAttacked)
        {
            ///Attack code here

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            animator.SetBool("Attack", true);
            animator.SetFloat("MoveSpeed", 0);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
