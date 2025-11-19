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


[CreateAssetMenu(menuName="EnemyAI/Movement/ChasePlayer")]
public class ChasePlayer : ScriptableObject, IEnemyMovementBehavior
{
    public float speed = 3.0f;
    
    public void Move(Transform enemy, Rigidbody enemyRigidbody, Transform target)
    {
        // If there is no target set, the enemy has nothing to do.
        if (target == null) return;

        Vector3 direction = target.position - enemy.position;
        direction.y = 0.0f;
        direction.Normalize();

        Vector3 targetVelocity = direction * speed;

        targetVelocity.y = enemyRigidbody.linearVelocity.y;

        enemyRigidbody.linearVelocity = targetVelocity;
    }
}
