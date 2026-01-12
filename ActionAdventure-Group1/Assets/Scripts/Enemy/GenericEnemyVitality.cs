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
* 2025/12/03 | Noah Zimmerman  | added method to set game manager
* 2025/12/17 | Leyton McKinney | Add DamageBehavior
************************************************************/
 
using UnityEngine;
 

public class GenericEnemyVitality : CombatEntity
{
    private DamageTextSpawner damageTextSpawner;
    private EnemyAI enemyBehavior;

    public override void TakeDamage(WeaponData attack)
    {
        base.TakeDamage(attack);
        enemyBehavior.DamageBehavior();
        damageTextSpawner.Spawn(transform, attack, false, false);
    }

    protected override void Awake()
    {
        base.Awake();
        damageTextSpawner = GameObject.FindGameObjectWithTag("DamageTextSpawner").GetComponent<DamageTextSpawner>();
        enemyBehavior = GetComponent<EnemyAI>();
    }

    public void setGameManager(GameManager gameManager)
    {
        manager = gameManager;
    }
}
