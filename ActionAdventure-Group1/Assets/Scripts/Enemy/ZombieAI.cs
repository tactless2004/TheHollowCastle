/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: ZombieAI.cs
* DESCRIPTION: Reworked Melee (Zombie) AI.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/17 | Leyton McKinney | Init
* 2026/01/16 | Leyton McKinney | Add isDead flag to fix some Object lifetime bugs
*
************************************************************/

using UnityEngine;
using UnityEngine.AI;


public class ZombieAI : EnemyAI
{
    [SerializeField] private GameObject player;

    [Header("Timers")]
    [SerializeField] private float idleTimer = 0.0f;
    [SerializeField] private float damageStunTime = 0.5f;
    [SerializeField] private float postAttackIdleTime = 0.5f;

    [Header("Combat")]
    [SerializeField] private ScriptableObject weaponSO;
    private WeaponData weapon;

    [Header("House Keeping")]
    [SerializeField, Tooltip("This is the maximum distance the enemy can be away from the player before the enemy considers despawning")]
    private float maxPlayerDistance = 10.0f;
    [SerializeField, Tooltip("This is the maximum amount of time (in seconds) that the enemy can be more than maxPlayerDistance away from the Player")]
    private float playerProximityTimeout = 5.0f; 
    private float playerProximityTimer;



    [Header("Movement")]

    [SerializeField] NavMeshAgent navMeshAgent;

    private bool isDead = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError($"Enemy {name} could not locate a reference to the player.");
            Destroy(gameObject);
        }

        navMeshAgent = GetComponent<NavMeshAgent>();

        weapon = weaponSO as WeaponData;
        playerProximityTimer = playerProximityTimeout;
    }
    private void Update()
    {
        if (player == null) ReacquirePlayer();

        // If this script is still running, and the Enemy is Dead just bail.
        if (isDead) return;

        // If the enemy is idle decrement the idleTimer, then exit.
        if (idleTimer > 0.0f)
        {
            idleTimer -= Time.deltaTime;
            // Enemies should not move if they're idle
            navMeshAgent.SetDestination(transform.position);
            return;
        }

        playerProximityTimer -= Time.deltaTime;

        if (playerProximityTimer <= 0.0f) Die();

        // 1.) Try Attack
        if (Vector3.Distance(transform.position, player.transform.position) <= weapon.range)
        {
            Attack();
            idleTimer = postAttackIdleTime;
            return;
        }

        // 2.) If not close enough to attack seek player
        SeekPlayer();

        // 3.) Check if can see player
        if (Vector3.Distance(transform.position, player.transform.position) <= maxPlayerDistance)
            playerProximityTimer = playerProximityTimeout;
    }

    private void ReacquirePlayer() {
        player = GameObject.FindGameObjectWithTag("Player");
        // If reaquiring a reference to the player fails, then there is no target and the Enemy should be destroyed
        if(player == null) Die();
    }

    public override void DamageBehavior()
    {
        idleTimer = damageStunTime;
    }

    private void Attack()
    {
        // make the enemy stop moving before attack
        navMeshAgent.SetDestination(transform.position);

        RaycastHit[] hits = Physics.RaycastAll(
            transform.position, // origin
            transform.forward, // direction
            weapon.range // distance
        );

        foreach(RaycastHit hit in hits)
        {
            // If hit object is self, don't attempt to damage self
            if (hit.transform.gameObject == this) continue;
            
            if (
                hit.collider.TryGetComponent( out CombatEntity combatEntity) &&
                hit.collider.CompareTag("Player")
            )
            {
                combatEntity.TakeDamage(weapon);
            }
        }
    }

    private void SeekPlayer()
    {
        if (!isDead)
            navMeshAgent.SetDestination(player.transform.position);
    }

    private void Die()
    {
        // If already dead, refrain from dying again
        if (isDead) return;

        isDead = true;
        Destroy(gameObject);
    }
}
