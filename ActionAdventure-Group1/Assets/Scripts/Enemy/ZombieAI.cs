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



    [Header("Movement")]

    [SerializeField] NavMeshAgent navMeshAgent;

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
    }
    private void Update()
    {
        if (player == null) ReacquirePlayer();
        // If the enemy is idle decrement the idleTimer, then exit.
        if (idleTimer > 0.0f)
        {
            idleTimer -= Time.deltaTime;
            return;
        }

        // 1.) Try Attack
        if (Vector3.Distance(transform.position, player.transform.position) <= weapon.range)
        {
            Attack();
            idleTimer = postAttackIdleTime;
            return;
        }

        // 2.) If not close enough to attack seek player
        SeekPlayer();
    }

    private void ReacquirePlayer() {
        player = GameObject.FindGameObjectWithTag("Player");
        // If reaquiring a reference to the player fails, then there is no target and the Enemy should be destroyed
        if(player == null) Destroy(gameObject);
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
        navMeshAgent.SetDestination(player.transform.position);
    }
}
