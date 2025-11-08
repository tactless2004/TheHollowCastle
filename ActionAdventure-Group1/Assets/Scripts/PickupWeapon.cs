/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: PickupItem.cs
* DESCRIPTION: Allows an item to be picked up by PlayerInventory.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
using TMPro;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private ScriptableObject weaponSO;

    public WeaponData GetWeapon()
    {
        return weaponSO as WeaponData;
    }

    public void SetWeapon(WeaponData weapon)
    {
        Transform trans = transform;
        Destroy(gameObject);
        Instantiate(weapon.pickupModelPrefab, trans.position, trans.rotation);
    }
}
