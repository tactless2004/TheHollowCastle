/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: EnemyMeleeAttackAI.cs
* DESCRIPTION: 
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
*
************************************************************/

using UnityEditor.Rendering;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyAI/Attack/Melee")]
public class EnemyMeleeAttackAI : ScriptableObject, IEnemyAttackBehavior
{
    public float range = 2.0f;
    public float damage = 10.0f;
    public float cooldown = 1.0f;
    public DamageType damageType = DamageType.Normal;

    private float lastAttackTime;

    public void Attack(Transform enemy, Transform target)
    {
        // If there's nothing to attack, bail
        if (target == null) return;

        if (Time.time - lastAttackTime < cooldown) return;

        if (Vector3.Distance(enemy.position, target.position) <= range)
        {
            if (target.TryGetComponent<IDamageable>(out IDamageable targetDamageable))
            {
                Vector3 direction = (target.position - enemy.position).normalized;
                AttackData attack = new AttackData(damage, damageType, enemy.gameObject, direction);
                targetDamageable.TakeDamage(attack);
                lastAttackTime = Time.time;
            }
        }
    }
}
