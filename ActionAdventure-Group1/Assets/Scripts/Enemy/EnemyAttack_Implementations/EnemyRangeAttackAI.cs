/************************************************************
* COPYRIGHT: 2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: EnemyRangeAttackAI.cs
* DESCRIPTION: IEnemyAttackBehavior implementation for ranged enemies.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;

[CreateAssetMenu(menuName="EnemyAI/Attack/Ranged")]
public class EnemyRangeAttackAI : ScriptableObject, IEnemyAttackBehavior
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float range = 10.0f;
    public float damage = 8.0f;
    public float cooldown = 2.0f;
    public DamageType damageType = DamageType.Normal;

    private float lastAttackTime;

    public void Attack(Transform enemy, Transform target)
    {
        // No target, bail
        if (target == null) return;

        if (Time.time - lastAttackTime < cooldown) return;

        if (Vector3.Distance(enemy.position, target.position) <= range)
        {
            Vector3 projectileDirection = (target.position - enemy.position).normalized;
            GameObject projectile = Instantiate(
                projectilePrefab,
                enemy.position + projectileDirection * 0.3f,
                Quaternion.LookRotation(projectileDirection)
            );

            if (projectile.TryGetComponent(out Projectile p))
            {
                AttackData attack = new AttackData(damage, damageType, enemy.gameObject, projectileDirection);
                p.Launch(projectileDirection, projectileSpeed, attack);
            }
            lastAttackTime = Time.time;
        }
    }
}
