/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: ChasePlayer.cs
* DESCRIPTION: IEnemyMovementBehavior implementation that chases the player.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName="EnemyAI/Movement/ChasePlayer")]
[RequireComponent(typeof(NavMeshAgent))]
public class ChasePlayer : ScriptableObject, IEnemyMovementBehavior
{
    public float speed = 3.0f;
    
    public void Move(Transform enemy, NavMeshAgent enemyNavMeshAgent, Transform target)
    {
        // If there is no target set, the enemy has nothing to do.
        if (target == null) return;

        enemyNavMeshAgent.SetDestination(target.position);
    }
}
