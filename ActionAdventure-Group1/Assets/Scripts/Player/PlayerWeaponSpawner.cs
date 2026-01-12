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
*
************************************************************/

using System.Collections.Generic;
using UnityEngine;
 

public class PlayerWeaponSpawner : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Vector3 weaponRotationOffset;
    private GameObject weaponInstance;

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponInstance != null) return; 
        weaponInstance = Instantiate(
            weapon, // GameObject
            weaponHolder.position, // Position
            Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0.0f, 180.0f, 0.0f),
            weaponHolder // Transform Parent
        );
        weaponInstance.SetActive(true);
    }

    public void DeleteAllWeapons()
    {
        Destroy(weaponInstance);
    }
}
