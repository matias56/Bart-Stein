using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;

    private float currentAttackCooldown;

    public float timeAttack;
    bool alreadyAttack;

    public float attackRange;

    public bool playerInRange;

    public LayerMask p;

    public GameObject projectile;

    public PlayerHealth pH;

    private void Awake()
    {

    }

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindObjectOfType<PlayerMovement>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        ApplyDifficultySettings();
    }

    private void Update()
    {
        if(enemyAwareness.isAggro && pH.health >0)
        {

            enemyNavMeshAgent.SetDestination(playersTransform.position);

            playerInRange = Physics.CheckSphere(transform.position, attackRange,p);

            if(playerInRange)
            {
                AttackPlayer();
            }

        }
        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }

        

        if (pH.defeated)
        {
            enemyAwareness.isAggro = false;
            Destroy(this);
            GetComponent<EnemyAI>().enabled = false;

        }

    }


    private void AttackPlayer()
    {
        enemyNavMeshAgent.SetDestination(transform.position);

        if(!alreadyAttack)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttack = true;
            Invoke(nameof(ResetAttack), currentAttackCooldown);
        }

       
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }

    void ApplyDifficultySettings()
    {
        DifficultySelector.Difficulty difficulty = DifficultySelector.Instance.currentDifficulty;

        switch (difficulty)
        {
            case DifficultySelector.Difficulty.Cool:
                
                currentAttackCooldown = timeAttack * 1.5f; // Slower attacks
                break;
            case DifficultySelector.Difficulty.Cowabunga:

                currentAttackCooldown = timeAttack * 1.2f;
                break;
            case DifficultySelector.Difficulty.WhoaMama:

                currentAttackCooldown = timeAttack; // Faster attacks
                break;

            case DifficultySelector.Difficulty.OhQuitIt:

                currentAttackCooldown = timeAttack * 0.7f; // Faster attacks
                break;

            case DifficultySelector.Difficulty.AwGeez:

                currentAttackCooldown = timeAttack * 0.5f; // Much faster attacks
                // Add more extreme behaviors here, e.g., dodging, special abilities
                break;
        }
    }

}
