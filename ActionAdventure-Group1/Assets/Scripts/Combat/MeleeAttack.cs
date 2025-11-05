/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: MeleeAttack.cs
* DESCRIPTION: Handles the logic of applying a melee attack.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public class MeleeAttack : IAttack
{
    private float range;
    private float baseDamage;
    private DamageType damageType;

    // Constructor
    public MeleeAttack(float range, float baseDamage = 20.0f, DamageType type = DamageType.Normal)
    {
        this.range = range;
        this.baseDamage = baseDamage;
        this.damageType = type;
    }

    public void Attack(Vector3 origin, Vector3 direction, GameObject source)
    {
        // Raycast out from player to player.facing
        if(Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            // Check if the hit GameObject is an IDamageable, if so it is an enemy
            // (or the environment which can also be damaged maybe XD)
            if (hit.collider.TryGetComponent(out IDamageable target))
            {
                AttackData attack = new AttackData(baseDamage, damageType, source, direction);
                target.TakeDamage(attack);
            }
        }
    }
}
