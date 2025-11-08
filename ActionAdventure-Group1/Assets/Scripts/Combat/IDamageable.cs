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
* 2025/11/07 | Leyton McKinney | Change IDamegable to take a WeaponData, instead of AttackData.
*
************************************************************/
 
using UnityEngine;


public interface IDamageable
{
    void TakeDamage(WeaponData attack);
}
