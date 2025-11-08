/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: EnemyAttackAI.cs
* DESCRIPTION: 
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Leyton Mckinney | Pivot to Just EnemyAttackAI, for the new combat system.
*
************************************************************/

using UnityEditor.Rendering;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyAI/Attack/Melee")]
public class EnemyAttackAI : ScriptableObject, IEnemyAttackBehavior
{
    private ScriptableObject weapon;

    public void Attack(Transform enemy, Transform target)
    {
        if (weapon == null || target == null) return;
        // This is among the worst ways to do this, but it works *_____*
        new Weapon(weapon as WeaponData).Attack(enemy.position, target.position, enemy.gameObject);
    }
}
