/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: Patrol.cs
* DESCRIPTION: IEnemyMovementBehavior implementation that patrols different waypoints in order.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/

using UnityEngine;
using UnityEngine.AI;
using System; // For NotImplementedException

[CreateAssetMenu(menuName = "EnemyAI/Movement/Patrol")]
public class Patrol : ScriptableObject, IEnemyMovementBehavior
{
    public Transform[] waypoints;
    public float speed = 3.0f;
    private int currentWaypoint = 0;

    public void Move(Transform enemy, NavMeshAgent enemyNavMeshAgent, Transform target)
    {
        throw new NotImplementedException("Patrol behavior is not implemented");
    }
}
