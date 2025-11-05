/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: IEnemyAttackBehavior.cs
* DESCRIPTION: Enemy attack behaviour interface for different enemy attack patterns.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
*
************************************************************/
 
using UnityEngine;
 

public interface IEnemyAttackBehavior
{
    void Attack(Transform enemy, Transform target, ref float lastAttackTime);
}
