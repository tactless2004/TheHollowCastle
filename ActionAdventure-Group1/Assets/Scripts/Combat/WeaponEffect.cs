/************************************************************
* COPYRIGHT:    2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: WeaponEffect.cs
* DESCRIPTION: ABC for Weapon Effects such as Life Steal, Freeze, Chain Lightning, things like that.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/07 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public abstract class WeaponEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject attacker, GameObject target, WeaponData attack);
}
