/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: GenericEnemyAI.cs
* DESCRIPTION: Generic enemy AI used for the Move, Attack pattern.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public class GenericEnemyAI : MonoBehaviour
{
    public IEnemyMovementBehavior movementBehavior;
    public IEnemyAttackBehavior attackBehavior;

    public Transform target;

    private void Update()
    {
        movementBehavior?.Move(transform, target);
        attackBehavior?.Attack(transform, target);
    }
}
