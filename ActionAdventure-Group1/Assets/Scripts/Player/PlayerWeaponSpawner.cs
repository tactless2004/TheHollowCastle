/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: PlayerWeaponSpawner.cs
* DESCRIPTION: Spawns Weapon models during Attack Animations.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/18 | Leyton McKinney | Init
* 2026/01/11 | Leyton McKinney | Changed public interface to Attack instead of Spawn Weapon
* 2026/01/12 | Leyton McKinney | Add OnAttackPerformed event.
*
************************************************************/

using System;
using UnityEngine;
 

public class PlayerWeaponSpawner : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Vector3 weaponRotationOffset;
    private GameObject weaponInstance;
    private PlayerContext player;

    public event Action<WeaponData> OnAttackPerformed;
    private void Awake()
    {
        if(!TryGetComponent(out player))
        {
            Debug.LogError("PlayerWeaponSpawner could not find PlayerContext Component.");
        }

        player.animation.OnAttackCancelled += DeleteAllWeapons;
    }
    public void Attack(WeaponData weapon)
    {
        // If there is already a weapon instance, don't spawn another.
        if (weaponInstance != null) return;

        var weaponPrefab = weapon.combatPrefab;

        weaponInstance = Instantiate(
            weaponPrefab, // GameObject
            weaponHolder.position, // Position
            Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0.0f, 180.0f, 0.0f),
            weaponHolder // Transform Parent
        );
        weaponInstance.SetActive(true);

        // Raise attack event now that the weapon has been spawned
        OnAttackPerformed?.Invoke(weapon);
    }

    public void DeleteAllWeapons()
    {
        Destroy(weaponInstance);
    }
}
