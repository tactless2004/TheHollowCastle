/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: EnemyAI.cs
* DESCRIPTION: Abstract class for Enemy Behavior.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/17 | Leyton McKinney | Init
* 2025/01/13 | Leyton McKinney | Add OnAttackPerformed event
*
************************************************************/

using System;
using UnityEngine;
 

public abstract class EnemyAI : MonoBehaviour
{
    public event Action<WeaponData> OnAttackPerformed;
    public abstract void DamagedBehavior();

    protected void RaiseAttackPerformed(WeaponData weapon)
    {
        OnAttackPerformed?.Invoke(weapon);
    }
}
