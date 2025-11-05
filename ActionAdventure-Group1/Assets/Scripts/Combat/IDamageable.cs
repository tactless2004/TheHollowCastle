/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: IDamageable.cs
* DESCRIPTION: Interface for enemies to take damage.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;


public interface IDamageable
{
    void TakeDamage(AttackData attack);
}
