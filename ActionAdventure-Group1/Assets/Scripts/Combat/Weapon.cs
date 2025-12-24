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
* 2025/11/17 | Leyton McKinney | Additional null checking to prevent WebGL crashes.
* 2025/12/23 | Leyton McKinney | Rework towards hitbox/hurtbox system.
************************************************************/

using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.ConstrainedExecution;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour
{

    [SerializeField] private ScriptableObject weaponDataSO;
    private WeaponData weapon;
    
    private void Awake()
    {
        weapon = weaponDataSO as WeaponData;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if there is now weapon or the weapon hit the player, bail.
        if (weapon == null||other.CompareTag("Player")) return;
        
        if(!other.TryGetComponent(out CombatEntity enemy)) return;

        enemy.TakeDamage(weapon);
        Debug.Log($"{name} hit {other.name}");
    }
}
