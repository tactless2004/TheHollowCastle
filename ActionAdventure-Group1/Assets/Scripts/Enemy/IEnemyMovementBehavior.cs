/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: IEnemyMovementBehavior.cs
* DESCRIPTION: Interface for enemy movement patterns.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public interface IEnemyMovementBehavior
{
    void Move(Transform enemy, Transform target);
}
