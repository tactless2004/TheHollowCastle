/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: Weapon.cs
* DESCRIPTION: Describes.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/07 | Leyton McKinney | Init
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

    public void Attack(Vector3 origin, Vector3 direction, GameObject source)
    {
        if (Time.time - lastAttackTime < weapon.attackCooldown) return;
        lastAttackTime = Time.time;

        // Melee Attack
        if (weapon.category == WeaponCategory.Melee)
        {
            // If hit on melee
            if(Physics.Raycast(origin, direction, out RaycastHit hit, weapon.range))
            {
                if (hit.collider.TryGetComponent(out CombatEntity target)) target.TakeDamage(weapon);
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
                projLaunch.Launch(direction, weapon.projectileSpeed, weapon);
            }

            else
            {
                Debug.LogError("Projectile instantiated without Projectile component. Destroying...");
                GameObject.Destroy(projectile);
            }
        }
    }
}
