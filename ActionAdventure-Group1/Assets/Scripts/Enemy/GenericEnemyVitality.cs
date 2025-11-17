/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: GenericEnemyVitality.cs
* DESCRIPTION: CombatEntity implementation for a generic enemy, no overrides.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/15 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public class GenericEnemyVitality : CombatEntity
{
    private DamageTextSpawner damageTextSpawner;

    public override void TakeDamage(WeaponData attack)
    {
        base.TakeDamage(attack);
        damageTextSpawner.Spawn(transform, attack, false, false);
    }

    protected override void Awake()
    {
        base.Awake();
        damageTextSpawner = GameObject.FindGameObjectWithTag("DamageTextSpawner").GetComponent<DamageTextSpawner>();
    }
}
