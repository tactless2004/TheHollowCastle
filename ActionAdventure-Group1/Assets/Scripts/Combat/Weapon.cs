/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: Weapon.cs
* DESCRIPTION: Weapon physics implementations for melee and ranged.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/07 | Leyton McKinney | Init
* 2025/11/11 | Leyton McKinney | Projectiles are not instantiated at source.transfrom instead of origin.
* 2025/11/12 | Leyton McKinney | Add targetTag, so enemy attacks do not hit enemies, and player attacks do not hit player.
*
************************************************************/

using System.Runtime.CompilerServices;
using UnityEngine;
 

// This is intentionally not a MonoBehavior as it doesn't implement any MonoBehavior methods.
public class Weapon
{
    private float lastAttackTime;
    private WeaponData weapon;

    public Weapon(WeaponData weapon)
    {
        this.weapon = weapon;
    }

    public WeaponData getWeaponData()
    {
        return weapon;
    }

    public void Attack(Vector3 origin, Vector3 direction, GameObject source, string targetTag)
    {
        if (Time.time - lastAttackTime < weapon.attackCooldown) return;
        lastAttackTime = Time.time;

        // Melee Attack
        if (weapon.category == WeaponCategory.Melee)
        {
            RaycastHit[] hits;
            // If hit on melee
            hits = Physics.RaycastAll(origin, direction, weapon.range);
            // Check if each RaycastHit is a valid target, if so apply damage.
            foreach (RaycastHit hit in hits) {
                if (
                    hit.collider.TryGetComponent(out CombatEntity target) && // hit a damgeable entity?
                    hit.collider.CompareTag(targetTag) // is the entity of the target tag?
                )
                {
                    target.TakeDamage(weapon);
                }
            }
        }

        // Ranged Attack
        else
        {
            if (weapon.projectilePrefab == null)
            {
                Debug.LogError("Range Attack without a Projectile Prefab. Check the Weapon Data file!");
            }

            // Create projectile gameobject
            GameObject projectile = GameObject.Instantiate(
                  weapon.projectilePrefab,
                  origin,
                  Quaternion.LookRotation(direction)
            );

            if (projectile.TryGetComponent(out Projectile projLaunch))
            {
                projLaunch.Launch(direction, weapon.projectileSpeed, weapon, targetTag);
            }

            else
            {
                Debug.LogError("Projectile instantiated without Projectile component. Destroying...");
                GameObject.Destroy(projectile);
            }
        }
    }
}
