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

using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyAI/Movement/Patrol")]
public class Patrol : ScriptableObject, IEnemyMovementBehavior
{
    public Transform[] waypoints;
    public float speed = 3.0f;
    private int currentWaypoint = 0;

    public void Move(Transform enemy, Transform target)
    {
        // No waypoints -> nothing to patrol
        if (waypoints.Length == 0) return;

        Transform waypoint = waypoints[currentWaypoint];
        Vector3 direction = (waypoint.position - enemy.position).normalized;
        enemy.position += direction * speed * Time.deltaTime;

        // cycle through waypoints if the player is reached
        if (Vector3.Distance(enemy.position, waypoint.position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}
